using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.Models
{
    public class RezerwacjaZlozona
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public int SeansId { get; set; }

        public virtual Miejsce Miejsce { get; set; }
    }
}