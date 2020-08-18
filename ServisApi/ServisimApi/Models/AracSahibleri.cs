using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class AracSahibleri
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
