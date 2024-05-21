using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class Rate
    {
        public int idRate { get; set; }

        public string descripcion { get; set; }

        public bool defaultRate { get; set; }
    }
}