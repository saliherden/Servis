using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class SeyahatListeleModel
    {
        public int Id { get; set; }
        public string AracPlaka { get; set; }
        public string SurucuAdiSoyadi { get; set; }
        public string KullaniciAdiSoyadi { get; set; }
        public string RotaSaati { get; set; }
        public int? RotaId { get; set; }
        public string DurakAdiSaati { get; set; }
        public string FirmaAdi { get; set; }
        public string SeyahatTarihi { get; set; }
        public bool? IptalDurum { get; set; }
        public bool? BindiDurum { get; set; }
        public bool? IndiDurum { get; set; }

        //public List<RotaAraci> rotaAraclari { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
