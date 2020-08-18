using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class RotaSaatleri
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public TimeSpan? ServisSaati { get; set; }
        public int? KategoriId { get; set; }
        public int? RotaId { get; set; }
        public int? FirmaId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
