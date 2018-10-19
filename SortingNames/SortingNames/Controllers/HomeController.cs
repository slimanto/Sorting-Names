using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SortingNames.Data.Models;
using SortingNames.Data.Repositories;
using System.Configuration;
using System.IO;

namespace SortingNames.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload()
        {
            string fullPath = string.Empty;
            List<PersonModel> people = new List<PersonModel>();
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != "")
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    fullPath = Path.Combine(path, filename);
                    Request.Files[upload].SaveAs(fullPath);
                }
            }

            return RedirectToAction("SortPerson", "Home", new { fullPath = fullPath });
        }

        public ActionResult SortPerson(string fullPath)
        {
            SortingNameRepository sortingNames = new SortingNameRepository();
            string savedFolder = ConfigurationManager.AppSettings["FilePath"];
            string fileName = ConfigurationManager.AppSettings["FileName"];

            var model = sortingNames.SortNames(fullPath, savedFolder, fileName);

            return View("Index", model);
        }
    }
}