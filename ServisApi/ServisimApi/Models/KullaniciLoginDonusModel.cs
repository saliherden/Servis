using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class KullaniciLoginDonusModel:Kullanicilar
    {
        public Versions versions { get; set; }
        public string Token { get; set; }
    }
}
