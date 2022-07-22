using System.ComponentModel.DataAnnotations;

namespace Smarticube.API.HKJC.Models
{
    public class hkjcDataPool_result
    {
        [Key]
        public string weekday { get; set; }
        [Key]
        public string matchname { get; set; }
        [Key]
        public DateTime matchdt { get; set; }
        
        public int? weekday_seq { get; set; }
        public string? HR1 { get; set; }
        public string? HR2 { get; set; }
        public string? FR1 { get; set; }
        public string? FR2 { get; set; }

        public string? FR_HAD { get; set; }
        public DateTime? createdon { get; set; }
        public DateTime? updatedon { get; set; }

        [Timestamp]
        public byte? timestamp { get; }
    }
}
