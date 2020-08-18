using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class Rotaver
    {
        public List<RotaDuraklariTimeString> duraklar = new List<RotaDuraklariTimeString>();
        public string[][] Rotadon { get; set; }
    }
}
