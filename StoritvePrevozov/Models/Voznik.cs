using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class Voznik
    {
        public int IDVoznik { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
    }
}