using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.Models
{
    public class SeansViewModel
    {
        public string Godzina { get; set; }
        public string Data { get; set; }
        public string Opis { get; set; }
        public string NazwaSali { get; set; }
        public string TytułFilmu { get; set; }
        public string RokFilmu { get; set; }
        public string RezyserFilmu { get; set; }
        public string OpisFilmu { get; set; }
        public int SalaId { get; set; }
        
    }
}