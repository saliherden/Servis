using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class RotaDuraklari
    {
        public int Id { get; set; }
        public decimal? DurakLat { get; set; }
        public decimal? DurakLong { get; set; }
        public TimeSpan? DurakSaati { get; set; }
        public string DurakIsmi { get; set; }
        public int? RotaId { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
