using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.ViewModels
{
    public class RepertoryViewModel
    {
        public int Id { get; set; }
        public string Godzina { get; set; }
        public string GodzinaZakonczenia { get; set; }
        public string Data { get; set; }
        public string Opis { get; set; }    
        public string Film { get; set; }
        public int SalaId { get; set; }
        
    }
}