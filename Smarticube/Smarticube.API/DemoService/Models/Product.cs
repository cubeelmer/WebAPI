using System.ComponentModel.DataAnnotations;

namespace Smarticube.API.DemoService.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string? LongDesc { get; set; }
        public string? ShortDesc { get; set; }
        public float? WeightValue { get; set; }
        public string? WeightUnit { get; set; }
        public string? Category { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; } = true;
        public ICollection<ProductItem> ProductItems { get; set; }
    }
}
