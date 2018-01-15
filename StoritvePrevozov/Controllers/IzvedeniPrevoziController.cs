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
    public class IzvedeniPrevoziController : Controller
    {
        // GET: IzvedeniPrevozi
        public ActionResult IzvedeniPrevozi()
        {
            var prevozi = pridobiIzvedenePrevoze().ToList();
            return View(prevozi);
        }

        public ActionResult Oceni(int id)
        {
            IzvedenPrevoz prevoz = pridobiIzvedenePrevoze().Where(x => x.IDIzvedenPrevoz == id).First();
            if (prevoz.Komentar==null || prevoz.OcenaPrevoza==0)
            {
                return View(prevoz);
                
            }
            else
            {
                return RedirectToAction("IzvedeniPrevozi");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Oceni([Bind(Include = "IDIzvedenPrevoz,DejanskiDatumOd,DejanskiDatumDo,DejanskoSteviloLjudi,DejanskiEMSOgosta," +
            "DejanskaZacetnaLokacija,DejanskaKoncnaLokacija,OcenaPrevoza,Komentar,IDNarocenPrevoz")] IzvedenPrevoz izvedenPrevoz, string komentar, string ocena)
        {
            
            urediIzvedenPrevoz(izvedenPrevoz, komentar, ocena);
            return RedirectToAction("IzvedeniPrevozi");
        }

        private void urediIzvedenPrevoz(IzvedenPrevoz izvedenPrevoz, string komentar, string ocena)
        {
            izvedenPrevoz = pridobiIzvedenePrevoze().Where(x => x.IDIzvedenPrevoz == izvedenPrevoz.IDIzvedenPrevoz).First();
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/IzvedenPrevoz", Method.PUT);
            izvedenPrevoz.Komentar = komentar;
            izvedenPrevoz.OcenaPrevoza = int.Parse(ocena);
            //request.AddQueryParameter("IDvoznika", "1");
            string json = JsonConvert.SerializeObject(izvedenPrevoz, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;
            
        }

        private List<IzvedenPrevoz> pridobiIzvedenePrevoze()
        {
            var client = new RestClient("http://soa.informatika.uni-mb.si/P8_StoritvePrevozov/v1/P8_StoritevPrevozovRest.svc");
            var request = new RestRequest("/GetIzvedeniPrevozi", Method.GET);
            //request.AddQueryParameter("IDvoznika", "1");

            var response = client.Execute(request);
            var content = response.Content;
            List<IzvedenPrevoz> seznamPrevozov = JsonConvert.DeserializeObject<List<IzvedenPrevoz>>(content);
            if (seznamPrevozov == null)
                return new List<IzvedenPrevoz>();
            return seznamPrevozov;
        }
    }
}