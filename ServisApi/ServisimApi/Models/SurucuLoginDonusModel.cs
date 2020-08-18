using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class SurucuLoginDonusModel
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }

        public Versions versions { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int AracID { get; set; }
    }
}
