using PF_WEB_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PF_WEB_MVC.Controllers
{
    public class ZapatosController : Controller
    {
        // GET: Zapatos
        
        public ActionResult Index(string codigo)
        {
            IEnumerable<ZapatosModel> zapList;
            if (String.IsNullOrEmpty(codigo))
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Zapatos").Result;
                zapList = response.Content.ReadAsAsync<IEnumerable<ZapatosModel>>().Result;                
            }
            else
            {
                HttpResponseMessage response2 = GlobalVariables.WebApiClient.GetAsync("Zapatos/"+codigo).Result;
                zapList = response2.Content.ReadAsAsync<IEnumerable<ZapatosModel>>().Result;
            }            
            return View(zapList);


        }        
        
    }
}