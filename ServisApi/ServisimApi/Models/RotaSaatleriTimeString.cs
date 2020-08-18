using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServisimApi.Models
{
    public class RotaSaatleriTimeString
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string ServisSaati { get; set; }
        public int? RotaId { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
