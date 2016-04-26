using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.Models
{
    public class Miejsce
    {
        public int Id { get; set; }
        public int Numer { get; set; }
        public int Rząd { get; set; }
        public int SalaId { get; set; }
        public int RezerwacjaZlozonaId { get; set; }
        public int RezerwacjaPrzyjetaId { get; set; }

        public static implicit operator Miejsce(int v)
        {
            throw new NotImplementedException();
        }
    }
}