using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class Araclar
    {
        public int Id { get; set; }
        public string PlakaNo { get; set; }
        public string DeviceId { get; set; }
        public int? SurucuId { get; set; }
        public int? FirmaId { get; set; }
        public int? AracSahibiId { get; set; }
        public bool? AracStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
