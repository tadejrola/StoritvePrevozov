using Newtonsoft.Json;
using P8_StoritvePrevozovREST.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoritvePrevozov.Controllers
{
    public class NaroceniPrevoziController : Controller
    {
        // GET: NaroceniPrevozi
        public ActionResult NaroceniPrevozi()
        {
            var narocila = pridobiNarocenePrevoze().ToList().Where(x => x.Izveden == false).ToList(); ;
            return View(narocila);
        }
        public ActionResult NovoNarocilo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoNarocilo([Bind(Include = "IDNarocenPrevoz,DatumOd,DatumDo,SteviloLjudi,EMSOgosta,ZacetnaLokacija,KoncnaLokacija,Izveden")] NarocenPrevoz narocenPrevoz, string tipVozila)
        {
            dodajNarocenPrevoz(narocenPrevoz, tipVozila);
            return RedirectToAction("NaroceniPrevozi");
        }

        

        public ActionResult Uredi(int id)
        {
            NarocenPrevoz narocilo = pridobiNarocenePrevoze().Where(x => x.IDNarocenPrevoz == id).First();
            return View(narocilo);
        }
        public ActionResult Koncaj(int id)
        {
            NarocenPrevoz narocilo = pridobiNarocenePrevoze().Where(x => x.IDNarocenPrevoz == id).First();
            return View(narocilo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Uredi([Bind(Include = "IDNarocenPrevoz,DatumOd,DatumDo,SteviloLjudi,EMSOgosta,ZacetnaLokacija,KoncnaLokacija,Izveden")] NarocenPrevoz narocenPrevoz)
        {
            if (ModelState.IsValid)
            {
                posodobiNarocenPrevoz(narocenPrevoz);
                return RedirectToAction("NaroceniPrevozi");
            }
            return View(narocenPrevoz);
        }

        public ActionResult Izbrisi(int id)
        {
            var narocilo = pridobiNarocenePrevoze().Where(x => x.IDNarocenPrevoz == id).First();
            return View(narocilo);
        }

        [HttpPost]
        public ActionResult Izbrisi(NarocenPrevoz narocenPrevoz)
        {
            izbrisiNarocenPrevoz(narocenPrevoz);
            return RedirectToAction("NaroceniPrevozi");

        }

        [HttpPost]
        public ActionResult Koncaj(NarocenPrevoz narocenPrevoz)
        {
            NarocenPrevoz narocilo = pridobiNarocenePrevoze().Where(x => x.IDNarocenPrevoz==narocenPrevoz.IDNarocenPrevoz).First();
            koncajNarocenPrevoz(narocilo);
            return RedirectToAction("NaroceniPrevozi");

        }

        private void koncajNarocenPrevoz(NarocenPrevoz narocenPrevoz)
        {
            narocenPrevoz.Izveden = true;
            posodobiNarocenPrevoz(narocenPrevoz);
            IzvedenPrevoz izvedenPrevoz = new IzvedenPrevoz();
            izvedenPrevoz.DejanskaKoncnaLokacija = narocenPrevoz.KoncnaLokacija;
            izvedenPrevoz.DejanskaZacetnaLokacija = narocenPrevoz.ZacetnaLokacija;
            izvedenPrevoz.DejanskiDatumDo = narocenPrevoz.DatumDo;
            izvedenPrevoz.DejanskiDatumOd = narocenPrevoz.DatumOd;
            izvedenPrevoz.DejanskiEMSOgosta = narocenPrevoz.EMSOgosta;
            izvedenPrevoz.DejanskoSteviloLjudi = narocenPrevoz.SteviloLjudi;
            izvedenPrevoz.IDNarocenPrevoz = narocenPrevoz.IDNarocenPrevoz;
            
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/PostIzvedenPrevoz", Method.POST);
            string json = JsonConvert.SerializeObject(izvedenPrevoz, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
            
        }

        private void izbrisiNarocenPrevoz(NarocenPrevoz narocenPrevoz)
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/NarocenPrevoz", Method.DELETE);
            //request.AddQueryParameter("IDvoznika", "1");
            string json = JsonConvert.SerializeObject(narocenPrevoz, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
        }

        private List<NarocenPrevoz> pridobiNarocenePrevoze()
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/NarocenPrevoz", Method.GET);
            //request.AddQueryParameter("IDvoznika", "1");

            var response = client.Execute(request);
            var content = response.Content;
            List<NarocenPrevoz> seznamNarocil=JsonConvert.DeserializeObject<List<NarocenPrevoz>>(content);
            if (seznamNarocil == null)
                return new List<NarocenPrevoz>();
            return seznamNarocil;
        }
        private void dodajNarocenPrevoz(NarocenPrevoz narocenPrevoz, string tipVozila)
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/NarocenPrevoz", Method.POST);
            request.AddQueryParameter("tipVozila", tipVozila);
            string json = JsonConvert.SerializeObject(narocenPrevoz, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
        }
        private void posodobiNarocenPrevoz(NarocenPrevoz narocenPrevoz)
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/PutNarocenPrevoz", Method.PUT);
            //request.AddQueryParameter("IDvoznika", "1");
            string json = JsonConvert.SerializeObject(narocenPrevoz, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;

        }
    }
}