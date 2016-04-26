using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.ViewModels
{
    public class ReservationEditViewModel
    {
        public int IdRezerwacji { get; set; }
        public int IdSali { get; set; }
        public int Numer { get; set; }
        public int Rząd { get; set; }
    }
}