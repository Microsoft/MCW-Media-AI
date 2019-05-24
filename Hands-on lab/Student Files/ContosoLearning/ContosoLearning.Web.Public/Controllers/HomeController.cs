using ContosoLearning.Data;
using ContosoLearning.Web.Public.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContosoLearning.Web.Public.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = new HomeIndexModel();


            // Code Here
            
            
            return View(model);
        }

        public async Task<ActionResult> Video(string id)
        {
            var model = new HomeVideoModel();

            
            // Code Here


            if (model.Video == null)
            {
                throw new Exception("Video not found!");
            }

            return View(model);
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