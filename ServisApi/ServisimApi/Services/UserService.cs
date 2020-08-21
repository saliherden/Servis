using ServisimApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ServisimApi.Services
{

    public interface IUserService
    {
        AuthenticateResponse AuthenticateUser(Kullanicilar model);
        AuthenticateResponseSurucu AuthenticateSurucu(Suruculer model);


        IEnumerable<Kullanicilar> GetAllUser();
        IEnumerable<Suruculer> GetAllSurucu();
        Kullanicilar GetByIdUser(int id);
        Suruculer GetByIdSurucu(int id);
    }
    public class UserService : IUserService
    {
        private readonly ServisContext _context;


        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, ServisContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponseSurucu AuthenticateSurucu(Suruculer model)
        {
            var user = _context.Suruculer.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtTokenSurucu(user);

            return new AuthenticateResponseSurucu(user, token);
        }

        public AuthenticateResponse AuthenticateUser(Kullanicilar model)
        {
            var user = _context.Kullanicilar.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

       
        public IEnumerable<Suruculer> GetAllSurucu()
        {
            return _context.Suruculer.ToList();
        }

        public IEnumerable<Kullanicilar> GetAllUser()
        {
            return _context.Kullanicilar.ToList();
        }



        public Suruculer GetByIdSurucu(int id)
        {
            return _context.Suruculer.FirstOrDefault(x => x.Id == id);
        }

        public Kullanicilar GetByIdUser(int id)
        {
            return _context.Kullanicilar.FirstOrDefault(x => x.Id == id);
        }

        

        private string generateJwtToken(Kullanicilar user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        } 
        private string generateJwtTokenSurucu(Suruculer user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
