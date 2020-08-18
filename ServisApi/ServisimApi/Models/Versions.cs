using System;
using System.Collections.Generic;

namespace ServisimApi.Models
{
    public partial class Versions
    {
        public int Id { get; set; }
        public int? Version { get; set; }
        public int? Device { get; set; }
        public bool? Status { get; set; }
        public bool? Mandatory { get; set; }
        public string Link { get; set; }
    }
}
