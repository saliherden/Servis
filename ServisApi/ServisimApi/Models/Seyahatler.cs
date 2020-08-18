using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class Seyahatler
    {
        public int Id { get; set; }
        public int? AracId { get; set; }
        public int? SurucuId { get; set; }
        public int KullaniciId { get; set; }
        public int? RotaSaatId { get; set; }
        public int? RotaId { get; set; }
        public int? DurakId { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? SeyahatTarihi { get; set; }
        public bool? IptalDurum { get; set; }
        public bool? BindiDurum { get; set; }
        public bool? IndiDurum { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
