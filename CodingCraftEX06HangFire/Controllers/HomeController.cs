using CodingCraftEX06HangFire.ViewModels;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftEX06HangFire.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Importar(BdinViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Index");
            if (viewModel.Arquivo == null || viewModel.Arquivo.ContentLength <= 0) return View("Index");

            using (TextReader textReader = new StreamReader(viewModel.Arquivo.InputStream))
            {
                var motor = new MultiRecordEngine(typeof(ViewModels.Bovespa.Header), typeof(ViewModels.Bovespa.Detail));
                motor.RecordSelector = new RecordTypeSelector(Infraestrura.Bovespa.BovespaSelector.Selector);
                var registros = motor.ReadStream(textReader);                

                return View(registros.ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}