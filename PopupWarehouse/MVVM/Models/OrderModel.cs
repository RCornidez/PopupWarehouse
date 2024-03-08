using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int ProductId { get; set; } // Assuming you have a Product model

        [Required]
        public int Quantity { get; set; }
    }
}
