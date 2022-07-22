using System.ComponentModel.DataAnnotations;

namespace Smarticube.API.DemoService.Models
{
    public class ProductItem
    {
        [Key]
        public Guid ProductId { get; set; }
        [Key]
        public Guid ItemId { get; set; }
        public float? Qty { get; set; } = ((float)1.0);   
        
        public Item ItemInvolved { get; set; }
    }
}
