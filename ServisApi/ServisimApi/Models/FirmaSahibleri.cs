using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class FirmaSahibleri
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string CepTel { get; set; }
        public string SabitTel { get; set; }
        public string Adres { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
