using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class Voznik_Vozilo
    {
        public int IDVoznik_Vozilo { get; set; }
        public virtual Vozilo Vozilo { get; set; }
        public virtual Voznik Voznik { get; set; }
    }
}