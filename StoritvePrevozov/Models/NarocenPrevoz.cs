using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P8_StoritvePrevozovREST.Classes
{
    public class NarocenPrevoz
    {
        public int IDNarocenPrevoz { get; set; }
        [Display(Name = "Datum začetka:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]
        

        public DateTime DatumOd { get; set; }
        [Display(Name = "Datum konca:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]

        public DateTime DatumDo { get; set; }
        [Display(Name = "Število oseb:")]
        public int SteviloLjudi { get; set; }
        [Display(Name = "EMŠO gosta:")]
        public string EMSOgosta { get; set; }
        [Display(Name = "Lokacija pobiranja:")]
        public string ZacetnaLokacija { get; set; }
        [Display(Name = "Lokacija odlaganja:")]
        public string KoncnaLokacija { get; set; }

        public bool Izveden { get; set; }
    }
}