using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class RotaDuraklariTimeSurucuKulln
    {
        public int Id { get; set; }
        public decimal? DurakLat { get; set; }
        public decimal? DurakLong { get; set; }
        public string DurakSaati { get; set; }
        public string DurakIsmi { get; set; }

        public List<DuraktakilerModel> duraktakiler { get; set; }
        public int? RotaId { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
