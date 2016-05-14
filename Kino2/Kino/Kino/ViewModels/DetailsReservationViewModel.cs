using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.ViewModels
{
    public class DetailsReservationViewModel
    {
        public int IdRezerwacji { get; set; }
        public string Godzina { get; set; }
        public string Data { get; set; }
        public string Opis { get; set; }
        public string TytulFilmu { get; set; }
    }
}