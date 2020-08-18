using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class SurucuGecmisModel
    {
        public int ID { get; set; }
        public string SurucuAdiSoyadi { get; set; }
        public string AracPlakaNo { get; set; }
        public int BindiSayi { get; set; }
        public int IndiSayi { get; set; }
        public string SeyahatTarihi { get; set; }
        public string GüzergahAdi { get; set; }
    }
}
