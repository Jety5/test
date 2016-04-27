using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.Models
{
    public class Seans
    {
        public int Id { get; set; }
        public string Godzina { get; set; }
        public string Data { get; set; }
        public string GodzinaZakonczenia { get; set; }
        public string Opis { get; set; }       

        public int FilmId { get; set; }
        public int SalaId { get; set; }
        public virtual RezerwacjaPrzyjeta RezerwacjaPrzyjeta { get; set; }
        public virtual RezerwacjaZlozona RezerwacjaZlozona { get; set; }

    }
}