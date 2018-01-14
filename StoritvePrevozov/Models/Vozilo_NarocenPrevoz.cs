﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class Vozilo_NarocenPrevoz
    {
        public int IDVozilo_NarocenPrevoz { get; set; }
        public virtual Vozilo Vozilo { get; set; }
        public virtual NarocenPrevoz NarocenPrevoz { get; set; }
    }
}