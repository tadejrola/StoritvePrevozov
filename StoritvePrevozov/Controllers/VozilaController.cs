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
    public class VozilaController : Controller
    {
        // GET: Vozila
        public ActionResult Vozila()
        {
            var vozila = pridobiSeznamVozil();
            return View(vozila);
        }

        public ActionResult Uredi(int id)
        {
            var vozilo = pridobiSeznamVozil().Where(x => x.IDVozilo == id).First(); ;
            return View(vozilo);
        }
        public ActionResult NovoVozilo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoVozilo([Bind(Include = "IDVozilo,Znamka,Model,TipVozila,Kapaciteta")] Vozilo vozilo)
        {
            dodajNovoVozilo(vozilo);
            return RedirectToAction("Vozila");
        }

        private void dodajNovoVozilo(Vozilo vozilo)
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/PostVozilo", Method.POST);
            string json = JsonConvert.SerializeObject(vozilo, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Uredi([Bind(Include = "IDVozilo,Znamka,Model,TipVozila,Kapaciteta")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                posodobiVozilo(vozilo);
                return RedirectToAction("Vozila");
            }
            return View(vozilo);
        }

        private void posodobiVozilo(Vozilo vozilo)
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/PutVozilo", Method.PUT);
            //request.AddQueryParameter("IDvoznika", "1");
            string json = JsonConvert.SerializeObject(vozilo, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
        }

        public ActionResult Izbrisi(int id)
        {
            var vozilo = pridobiSeznamVozil().Where(x => x.IDVozilo == id).First(); ;
            return View(vozilo);
        }

        [HttpPost]
        public ActionResult Izbrisi(Vozilo vozilo)
        {
            izbrisiVozilo(vozilo);
            return RedirectToAction("Vozila");

        }

        private void izbrisiVozilo(Vozilo vozilo)
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/DeleteVozilo", Method.DELETE);
            //request.AddQueryParameter("IDvoznika", "1");
            string json = JsonConvert.SerializeObject(vozilo, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
        }

        private List<Vozilo> pridobiSeznamVozil()
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/Vozila", Method.GET);
            //request.AddQueryParameter("IDvoznika", "1");

            var response = client.Execute(request);
            var content = response.Content;
            List<Vozilo> seznamvozil = JsonConvert.DeserializeObject<List<Vozilo>>(content);
            return seznamvozil;
        }
    }
}