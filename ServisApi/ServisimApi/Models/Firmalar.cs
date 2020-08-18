using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class Firmalar
    {
        public int Id { get; set; }
        public string FrimaAdı { get; set; }
        public string FirmaAdresi { get; set; }
        public string FirmaTelefonu { get; set; }
        public int? FirmaSahibiId { get; set; }
        public string İl { get; set; }
        public string İlce { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
