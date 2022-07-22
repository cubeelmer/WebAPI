using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Smarticube.API.HKJC.Football.Models
{
    [Table("hkjcDataPool")]
    public partial class HkjcDataPool
    {
        [Key]
        [Column("weekday")]
        [StringLength(20)]
        public string Weekday { get; set; } = null!;
        [Key]
        [Column("matchtype")]
        [StringLength(50)]
        public string Matchtype { get; set; } = null!;
        [Key]
        [Column("matchdate")]
        [StringLength(30)]
        public string Matchdate { get; set; } = null!;
        [Key]
        [Column("matchname")]
        [StringLength(100)]
        public string Matchname { get; set; } = null!;
        [Column("hometeam")]
        [StringLength(100)]
        public string? Hometeam { get; set; }
        [Column("awayteam")]
        [StringLength(100)]
        public string? Awayteam { get; set; }
        [Column("weekday_seq")]
        public int? WeekdaySeq { get; set; }
        [Column("matchdt", TypeName = "datetime")]
        public DateTime? Matchdt { get; set; }
        [Column("fh")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fh { get; set; }
        [Column("fd")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fd { get; set; }
        [Column("fa")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fa { get; set; }
        [Column("h")]
        [StringLength(10)]
        [Unicode(false)]
        public string? H { get; set; }
        [Column("d")]
        [StringLength(10)]
        [Unicode(false)]
        public string? D { get; set; }
        [Column("a")]
        [StringLength(10)]
        [Unicode(false)]
        public string? A { get; set; }
        [Column("fh_up")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FhUp { get; set; }
        [Column("fd_up")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FdUp { get; set; }
        [Column("fa_up")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FaUp { get; set; }
        [Column("h_up")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HUp { get; set; }
        [Column("d_up")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DUp { get; set; }
        [Column("a_up")]
        [StringLength(10)]
        [Unicode(false)]
        public string? AUp { get; set; }
        [Column("matchdt_up", TypeName = "datetime")]
        public DateTime? MatchdtUp { get; set; }
        [Column("line")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Line { get; set; }
        [Column("big")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Big { get; set; }
        [Column("small")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Small { get; set; }
        [Column("hline")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hline { get; set; }
        [Column("hbig")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hbig { get; set; }
        [Column("hsmall")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hsmall { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T0 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T1 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T2 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T3 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T4 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T5 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? T6 { get; set; }
        [Column("T7_plus")]
        [StringLength(10)]
        [Unicode(false)]
        public string? T7Plus { get; set; }
        [Column("odd")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Odd { get; set; }
        [Column("even")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Even { get; set; }
        [Column("HH")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hh { get; set; }
        [Column("HD")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hd { get; set; }
        [Column("HA")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Ha { get; set; }
        [Column("DH")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Dh { get; set; }
        [Column("DD")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Dd { get; set; }
        [Column("DA")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Da { get; set; }
        [Column("AH")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Ah { get; set; }
        [Column("AD")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Ad { get; set; }
        [Column("AA")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Aa { get; set; }
        [Column("FR10")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr10 { get; set; }
        [Column("FR20")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr20 { get; set; }
        [Column("FR21")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr21 { get; set; }
        [Column("FR30")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr30 { get; set; }
        [Column("FR31")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr31 { get; set; }
        [Column("FR32")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr32 { get; set; }
        [Column("FR00")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr00 { get; set; }
        [Column("FR11")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr11 { get; set; }
        [Column("FR22")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr22 { get; set; }
        [Column("FR33")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr33 { get; set; }
        [Column("FR01")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr01 { get; set; }
        [Column("FR02")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr02 { get; set; }
        [Column("FR12")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr12 { get; set; }
        [Column("FR03")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr03 { get; set; }
        [Column("FR13")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr13 { get; set; }
        [Column("FR23")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Fr23 { get; set; }
        [Column("HR10")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr10 { get; set; }
        [Column("HR20")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr20 { get; set; }
        [Column("HR21")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr21 { get; set; }
        [Column("HR30")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr30 { get; set; }
        [Column("HR31")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr31 { get; set; }
        [Column("HR32")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr32 { get; set; }
        [Column("HR00")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr00 { get; set; }
        [Column("HR11")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr11 { get; set; }
        [Column("HR22")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr22 { get; set; }
        [Column("HR33")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr33 { get; set; }
        [Column("HR01")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr01 { get; set; }
        [Column("HR02")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr02 { get; set; }
        [Column("HR12")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr12 { get; set; }
        [Column("HR03")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr03 { get; set; }
        [Column("HR13")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr13 { get; set; }
        [Column("HR23")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hr23 { get; set; }
        [Column("HHA_HG")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HhaHg { get; set; }
        [Column("HHA_H")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HhaH { get; set; }
        [Column("HHA_D")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HhaD { get; set; }
        [Column("HHA_A")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HhaA { get; set; }
        [Column("HHA_AG")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HhaAg { get; set; }
        [Column("FTS_H")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FtsH { get; set; }
        [Column("FTS_N")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FtsN { get; set; }
        [Column("FTS_A")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FtsA { get; set; }
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
        [Column("createdby")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Createdby { get; set; }
        [Column("timestamp")]
        public byte[]? Timestamp { get; set; }
    }
}
