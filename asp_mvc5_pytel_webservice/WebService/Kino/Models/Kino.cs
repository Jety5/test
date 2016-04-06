using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kino.Models
{
    public class Kino
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Adres { get; set; }

        public virtual Sala Sala { get; set; }
    }
}