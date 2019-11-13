using System;
using System.Collections.Generic;
using System.Text;

namespace StaffTransferCore.Models.Valhalla.Entities
{
    public class Guest
    {
        /// <summary>
        /// Identificador del pasajero
        /// </summary>
        public int ID { get; set; }

        public string FullName { set; get; }
        /// <summary>
        /// If the guest is a child, (according to the data source), then this would be true
        /// </summary>
        public bool IsChild { set; get; }
        /// <summary>
        /// This will be NULL if Guest is NOT a child
        /// </summary>
        public int Age { set; get; }
    }
}
