using ContosoLearning.Data;
using ContosoLearning.Web.Admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContosoLearning.Web.Admin.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = new HomeIndexModel();

            var videoRepo = VideoRepositoryFactory.Create();

            model.Videos = from c in await videoRepo.GetAll()
                            orderby c.Title, c.Created
                            select c;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            // ======================================================================
            // Delete document from Cosmos DB
            // ======================================================================
            
            // Code Here


            // ======================================================================
            // Delete files from Blob Storage
            // ======================================================================

            // Code Here


            // ======================================================================
            // Delete video from Video Indexer service
            // ======================================================================

            // Code Here

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Upload()
        {
            var model = new HomeUploadModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file, HomeUploadModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Generate a unique id for the uploaded video
            var videoId = Guid.NewGuid().ToString();

            // ======================================================================
            // Upload Video File to Blob Storage
            // ======================================================================

            // Code Here


            // ======================================================================
            // Save Video info to Cosmos DB Database / Collection
            // ======================================================================
            
            // Code Here


            return RedirectToAction("Index");
        }
    }
}