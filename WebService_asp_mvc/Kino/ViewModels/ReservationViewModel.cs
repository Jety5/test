using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.ViewModels
{
    public class ReservationViewModel
    {
        public int IdRezerwacji { get; set; }
        public int NumerMiejsca { get; set; }
        public int Rząd { get; set; }

        public string NazwaSali { get; set; }
        public string Rzędy { get; set; }
        public int IloscMiejsc { get; set; }

        public int SalaId { get; set; }
        public string OpisRezerwacji { get; set; }
    }
}