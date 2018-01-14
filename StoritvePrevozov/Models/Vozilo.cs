using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class Vozilo
    {
        public int IDVozilo { get; set; }
        public string Znamka { get; set; }
        public string Model { get; set; }
        public string TipVozila { get; set; }
        public int Kapaciteta { get; set; }
    }
}