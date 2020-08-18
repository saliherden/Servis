using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class Rotalar
    {
        public int Id { get; set; }
        public string RotaIsmi { get; set; }
        public byte[] Rota { get; set; }
        public int KategoriId { get; set; }
        public int RotaSaatId { get; set; }
        public int FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
