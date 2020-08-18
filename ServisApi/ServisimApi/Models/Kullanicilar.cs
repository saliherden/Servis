using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class Kullanicilar
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public decimal? AdresLat { get; set; }
        public decimal? AdresLong { get; set; }
        public int? KategoriId { get; set; }
        public string DeviceId { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
