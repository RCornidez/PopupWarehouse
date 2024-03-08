using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime ShipmentDate { get; set; }
    }
}
