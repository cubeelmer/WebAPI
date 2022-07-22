using System.ComponentModel.DataAnnotations;

namespace Smarticube.API.DemoService.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public string? LongDesc { get; set; }
        public string? ShortDesc { get; set; }
        public string? Unit { get; set; }
        public string? Category { get; set; }        
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; } = true;

    }
}
