using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class RotaAraci
    {
        public int Id { get; set; }
        public int? RotaId { get; set; }
        public int SurucuId { get; set; }
        public int? AracId { get; set; }
        public DateTime RotaCikisTarih { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
