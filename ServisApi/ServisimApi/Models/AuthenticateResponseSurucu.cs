using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class AuthenticateResponseSurucu : KullaniciLoginDonusModel
    {

        public string Token { get; set; }


        public AuthenticateResponseSurucu(Suruculer user, string token)
        {

            Token = token;
        }
    }
    
}
