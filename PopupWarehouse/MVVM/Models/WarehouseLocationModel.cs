using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class WarehouseLocation
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string LocationName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
