using ServisimApi.Models;

namespace WebApi.Models
{
    public class AuthenticateResponse:KullaniciLoginDonusModel
    {
       
        public string Token { get; set; }


        public AuthenticateResponse(Kullanicilar user, string token)
        {
           
            Token = token;
        }
    }
}