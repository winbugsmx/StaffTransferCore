using StaffTransferCore.Business.Interfaces;
using StaffTransferCore.Service.Interfaces;
using StaffTransferCore.Models.OperacionesService;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using StaffTransferCore.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;
using StaffTransferCore.Models.Valhalla;
using StaffTransferCore.Models.Valhalla.Entities;
using StaffTransferCore.Models.Valhalla.Enums;
using Newtonsoft.Json;

namespace StaffTransferCore.Business
{
    public class BookingStaffProcess : IBookingStaffProcess
    {
        private readonly ILogger<BookingStaffProcess> _logger;
        private readonly AppSettings _appSettings;
        private readonly IOperacionesService _operacionesService;
        private readonly IValhallaService _valhallaService;

        public BookingStaffProcess(ILogger<BookingStaffProcess> logger,
                                   IOptions<AppSettings> configuration,
                                   IOperacionesService operacionesService,
                                   IValhallaService valhallaService)
        {
            _logger = logger;
            _appSettings = configuration.Value;
            _operacionesService = operacionesService;
            _valhallaService = valhallaService;
        }

        public async Task Run()
        {
            _logger.LogInformation("Inicia lectura de datos de tablas de configuración");
            //Llamar a OperacionesService para recuperar datos de tablas de configuración de reservas automáticas

            IEnumerable<LlegSalSTAFF> llegSals;
            try
            {
                llegSals = (await _operacionesService.GetLlegSalSTAFF())?.ToList();
                if (!llegSals.Any())
                {
                    _logger.LogInformation("No se encontró ninguna reserva con la cual trabajar");
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetLlegSalStaff-" + ex.Message);
                return;
            }

            IEnumerable<DetPaxSTAFF> detpaxs;
            try
            {
                detpaxs = (await _operacionesService.GetDetPaxSTAFF()).ToList();

                if (!detpaxs.Any())
                {
                    _logger.LogInformation("No se encontraron los detalles de las reservas a insertar");
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetDetPaxStaff-" + ex.Message);
                return;
            }

            //Mandar dichos datos a proceso que genere Request a Valhalla de creación de reservas
            _logger.LogInformation("Generando request a Valhalla");
            List<ListServiceResponse> ValhallaResponse = CreateValhallaBookings(llegSals, detpaxs);
            _logger.LogInformation("Request enviados y resueltos");

            foreach (ListServiceResponse ValhallaService in ValhallaResponse)
            {
                foreach (ErrorBooking booking in ValhallaService.ServiceResponse.errors)
                {
                    _logger.LogInformation(booking.bookingNumber + booking.code + booking.message + booking.subCode);
                }
            }

            //Recuperar lista de camionetas que armara el servicio de SIOR
            //IEnumerable<ICamionSTAFF> camions = (await _operacionesService.GetCamionSTAFF()).ToList();

        }

        public List<ListServiceResponse> CreateValhallaBookings(IEnumerable<LlegSalSTAFF> llegSals, IEnumerable<DetPaxSTAFF> detPaxes)
        {
            List<Booking> bookings = new List<Booking>();
            List<ListServiceResponse> responseList = new List<ListServiceResponse>();
            IEnumerable<DetPaxSTAFF> detPaxesOfLlegSal;
            foreach (LlegSalSTAFF llegSal in llegSals)
            {
                bookings = new List<Booking>();
                detPaxesOfLlegSal = detPaxes.Where(x => x.Clave_Lleg_Sal_auto == llegSal.Clave_LLeg_Sal_auto);
                bookings.Add(GetBooking(llegSal, detPaxesOfLlegSal));
                responseList.Add(new ListServiceResponse()
                {
                   ServiceResponse = _valhallaService.SendBooking(bookings).Result,
                   Clave_Lleg_Sal_auto = llegSal.Clave_LLeg_Sal_auto
                });
            }
            //var requestBody = JsonConvert.SerializeObject(new { bookings = bookings }, Formatting.Indented);
            return responseList;
        }

        public Booking GetBooking(LlegSalSTAFF llegSal, IEnumerable<DetPaxSTAFF> detPaxs)
        {
            int[] paxReference = GetPaxesReference(detPaxs);

            var booking = new Booking()
            {
                bookingInfo = new BookingInfo()
                {
                    bookingNumber = llegSal.Clave_LLeg_Sal_auto + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss"),
                    partnerCode = llegSal.Clave_Ag,
                    arrivalFlights = GetArrivalFlights(llegSal, detPaxs, paxReference),
                    departureFlights = GetDepartureFlights(llegSal, detPaxs, paxReference),
                    hotels = GetHoteles(detPaxs,paxReference),
                    paxs = GetPaxes(llegSal, paxReference)
                },
                bookingServices = new BookingServices()
                {
                    shuttleServices = GetShuttleServices(paxReference, llegSal, detPaxs)
                }
            };
            return booking;
        }

        public int[] GetPaxesReference(IEnumerable<DetPaxSTAFF> detPaxes)
        {
            int totalPax = detPaxes.Select(x => x.PersA).Sum();

            int[] paxesReference = Enumerable.Range(1, totalPax).ToArray();
            return paxesReference;
        }

        public int[] GetPaxesReferenceByConsec(DetPaxSTAFF detPax, int sumpaxs)
        {
            int[] paxesReferenceByConsec = Enumerable.Range(sumpaxs, detPax.PersA).ToArray();
            return paxesReferenceByConsec;
        }

        public ArrivalFlight[] GetArrivalFlights(LlegSalSTAFF llegSal, IEnumerable<DetPaxSTAFF> detPaxs, int[] paxReference)
        {
            if (llegSal.Vuelo_Llega !=null && llegSal.Hora_Llega != null)
            {
                return new ArrivalFlight[]
                {
                    new ArrivalFlight(){
                        arrivalAirportIataCode = llegSal.Clave_AeroIATA,
                        arrivalDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),
                        arrivalTime = llegSal.Hora_Llega.Value.ToString("HH:mm"),
                        flightReferenceId = 1,
                        airlineIataCode = llegSal.Vuelo_Llega.Substring(0,2),
                        flightNumber = llegSal.Vuelo_Llega.Substring(2, llegSal.Vuelo_Llega.Length - 2),
                        paxesReferences = paxReference
                    }
                };
            }
            return null;
        }

        public DepartureFlight[] GetDepartureFlights(LlegSalSTAFF llegSal, IEnumerable<DetPaxSTAFF> detPaxs, int[] paxReference)
        {
            if (llegSal.Vuelo_Sale != null && llegSal.Hora_Sale != null)
            {
                return new DepartureFlight[]
                {
                    new DepartureFlight(){
                        departureAirportIataCode = llegSal.Clave_AeroIATA,
                        departureDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),
                        departureTime = llegSal.Hora_Sale.Value.ToString("HH:mm"),
                        flightReferenceId = 1,
                        airlineIataCode = llegSal.Vuelo_Sale.Substring(0,2),
                        flightNumber = llegSal.Vuelo_Sale.Substring(2, llegSal.Vuelo_Sale.Length - 2),
                        paxesReferences = paxReference
                    }
                };
            }
            return null;
        }

        public Hotel[] GetHoteles(IEnumerable<DetPaxSTAFF> detpaxs, int[] paxReference)
        {
            Hotel[] hoteles = new Hotel[detpaxs.Count()];
            int sumpaxs;
            foreach(DetPaxSTAFF detPaxSTAFF in detpaxs)
            {
                sumpaxs = detpaxs.Where(x => x.Consec < detPaxSTAFF.Consec).Select(x => x.PersA).Sum() + 1;

                hoteles[detPaxSTAFF.Consec - 1] = new Hotel()
                {
                    hotelReferenceId = detPaxSTAFF.Consec,
                    checkIn = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),
                    checkOut = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),
                    paxesReferences = GetPaxesReferenceByConsec(detPaxSTAFF, sumpaxs),
                    bedaCode = detPaxSTAFF.HotelBedaCode,
                    name = detPaxSTAFF.NombreHotel
                };
            }
            return hoteles;
        }

        public Pax[] GetPaxes(LlegSalSTAFF llegSal, int[] paxReference)
        {
            Pax[] paxes = new Pax[paxReference.Count()];
            foreach (int pax in paxReference)
            {
                paxes[pax - 1] = new Pax()
                {
                    paxReferenceId = pax,
                    name = llegSal.Nombre,
                    isAdult = true,
                    isLead = pax == 1 ? true : false,
                    age = 0
                };
            }
            return paxes;
        }

        public ShuttleService[] GetShuttleServices(int[] paxReference, LlegSalSTAFF llegSal, IEnumerable<DetPaxSTAFF> detPaxs)
        {
            ShuttleService[] shuttleServices = new ShuttleService[detPaxs.Count()];
            foreach(DetPaxSTAFF detPaxSTAFF in detPaxs)
            {
                shuttleServices[detPaxSTAFF.Consec-1] = new ShuttleService()
                {
                    serviceReferenceId = detPaxSTAFF.Consec,
                    serviceNumber = detPaxSTAFF.Consec.ToString(),
                    paxesReferences = GetPaxesReferenceByConsec(detPaxSTAFF, (detPaxs.Where(x => x.Consec < detPaxSTAFF.Consec).Select(x => x.PersA).Sum()) + 1),
                    originShuttlePoint = GetOriginShuttlePoint(llegSal, detPaxSTAFF),
                    destinationShuttlePoint = GetDestinationShuttlePoint(llegSal, detPaxSTAFF),
                    isPrivate = false,
                    //shuttleDate = detPaxSTAFF.TipoServ == "I" && detPaxSTAFF.PickUp.Hour < 4 ? DateTime.Today.AddDays(1).ToString("yyyyMMdd") : DateTime.Today.AddDays(2).ToString("yyyyMMdd"),
                    shuttleDate = detPaxSTAFF.TipoServ == "I" && detPaxSTAFF.PickUp.Hour < 4 ? DateTime.Today.AddDays(2).ToString("yyyyMMdd") : DateTime.Today.AddDays(1).ToString("yyyyMMdd"),
                    pickupTime = detPaxSTAFF.PickUp.ToString("HH:mm")
                };
            }
            return shuttleServices;
        }

        public ShuttlePoint GetOriginShuttlePoint(LlegSalSTAFF llegSal ,DetPaxSTAFF detPax)
        {
            if (detPax.TipoServ == "R" && detPax.Lleg_Sal == "L")
            {
                return new ShuttlePoint()
                {
                    type = ShuttlePointType.AIRPORT,
                    code = llegSal.Clave_AeroIATA,
                    bedaCode = 0
                };
            }
            if ((detPax.TipoServ == "R" && detPax.Lleg_Sal == "S") || (detPax.TipoServ == "I"))
            {
                return new ShuttlePoint()
                {
                    type = ShuttlePointType.HOTEL,
                    code = detPax.HotelBedaCode.ToString(),
                    bedaCode = 0
                };
            }
            return null;
        }

        public ShuttlePoint GetDestinationShuttlePoint(LlegSalSTAFF llegSal, DetPaxSTAFF detPax)
        {
            if ((detPax.TipoServ == "R" && detPax.Lleg_Sal == "L") || (detPax.TipoServ == "I"))
            {
                return new ShuttlePoint()
                {
                    type = ShuttlePointType.HOTEL,
                    code = detPax.HotelBedaCode.ToString(),
                    bedaCode = 0
                };
            }
            if (detPax.TipoServ == "R" && detPax.Lleg_Sal == "S")
            {
                return new ShuttlePoint()
                {
                    type = ShuttlePointType.AIRPORT,
                    code = llegSal.Clave_AeroIATA,
                    bedaCode = 0
                };
            }
            return null;
        }

    }
}
