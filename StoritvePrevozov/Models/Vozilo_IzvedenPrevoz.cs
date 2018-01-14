using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class Vozilo_IzvedenPrevoz
    {
        public int IDVozilo_IzvedenPrevoz { get; set; }
        public virtual Vozilo Vozilo { get; set; }
        public virtual IzvedenPrevoz IzvedenPrevoz { get; set; }
    }
}