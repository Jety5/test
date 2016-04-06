using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Rzędy { get; set; }
        public int IloscMiejsc { get; set; }
        
        public int KinoId { get; set; }

        public virtual Seans Seans { get; set; }
        public virtual Miejsce Miejsce { get; set; }
    }
}