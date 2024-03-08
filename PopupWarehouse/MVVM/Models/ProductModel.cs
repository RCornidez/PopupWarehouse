using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
