using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class IzvedenPrevoz
    {
        public int IDIzvedenPrevoz { get; set; }
        [Display(Name = "Datum začetka:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]
        public DateTime DejanskiDatumOd { get; set; }
        [Display(Name = "Datum konca:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]
        public DateTime DejanskiDatumDo { get; set; }
        public int DejanskoSteviloLjudi { get; set; }
        public string DejanskiEMSOgosta { get; set; }

        public string DejanskaZacetnaLokacija { get; set; }
        public string DejanskaKoncnaLokacija { get; set; }

        public int OcenaPrevoza { get; set; }
        public string Komentar { get; set; }
        public int IDNarocenPrevoz { get; set; }
    }
}