using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Smarticube.API.HKJC.Football.Models
{
    [Table("hkjcDataPool_result")]
    public partial class HkjcDataPoolResult
    {
        [Key]
        [Column("weekday")]
        [StringLength(20)]
        public string Weekday { get; set; } = null!;
        [Key]
        [Column("matchname")]
        [StringLength(100)]
        public string Matchname { get; set; } = null!;
        [Key]
        [Column("matchdt", TypeName = "datetime")]
        public DateTime Matchdt { get; set; }
        [Column("weekday_seq")]
        public int? WeekdaySeq { get; set; }
        [Column("HR1")]
        [StringLength(4)]
        [Unicode(false)]
        public string? Hr1 { get; set; }
        [Column("HR2")]
        [StringLength(4)]
        [Unicode(false)]
        public string? Hr2 { get; set; }
        [Column("FR1")]
        [StringLength(4)]
        [Unicode(false)]
        public string? Fr1 { get; set; }
        [Column("FR2")]
        [StringLength(4)]
        [Unicode(false)]
        public string? Fr2 { get; set; }
        [Column("FR_HAD")]
        [StringLength(4)]
        [Unicode(false)]
        public string? FrHad { get; set; }
        [Column("createdon", TypeName = "datetime")]
        public DateTime? Createdon { get; set; }
        [Column("updatedon", TypeName = "datetime")]
        public DateTime? Updatedon { get; set; }
        [Column("timestamp")]
        public byte[]? Timestamp { get; set; }
    }
}
