using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class SurucuLoginPostModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AracPlaka { get; set; }
        public int Device { get; set; }
        public int Version { get; set; }
        
    }
}
