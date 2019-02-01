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

            var videoRepo = VideoRepositoryFactory.Create();

            model.Videos = (from v in await videoRepo.GetAll()
                            orderby v.Title, v.Created
                            select new VideoListModel
                            {
                                Video = v
                            }).ToArray();

            var client = new System.Net.Http.HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]);

            // Get Video Indexer Account ID
            var uriAccountsResponse = await client.GetAsync("https://api.videoindexer.ai/auth/trial/Accounts");
            var jsonUriAccountsResponse = await uriAccountsResponse.Content.ReadAsStringAsync();
            dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(jsonUriAccountsResponse);
            var videoIndexerAccountId = accounts[0].id;

            foreach (var v in model.Videos)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]);
                // Get Video Indexer Access Token
                var uriResponse = await client.GetAsync($"https://api.videoindexer.ai/auth/trial/Accounts/{videoIndexerAccountId}/Videos/{v.Video.VideoId}/AccessToken");
                var jsonUriResponse = await uriResponse.Content.ReadAsStringAsync();
                var accessToken = jsonUriResponse.Replace("\"", string.Empty);

                var uri = $"https://api.videoindexer.ai/trial/Accounts/{videoIndexerAccountId}/Videos/{v.Video.VideoId}/Index?accessToken={accessToken}";
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();

                dynamic breakdown = Newtonsoft.Json.Linq.JObject.Parse(json);

                var thumbnailId = breakdown?.summarizedInsights?.thumbnailId;
                v.ThumbnailUrl = $"https://api.videoindexer.ai/trial/Accounts/{videoIndexerAccountId}/Videos/{v.Video.VideoId}/Thumbnails/{thumbnailId}?accessToken={accessToken}&format=Jpeg";
            }

            return View(model);
        }

        public async Task<ActionResult> Video(string id)
        {
            var model = new HomeVideoModel();

            var courseRepo = VideoRepositoryFactory.Create();
            model.Video = await courseRepo.Get(id);

            if (model.Video == null)
            {
                throw new Exception("Video not found!");
            }

            // Get Access Token
            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]);

            var uriAccountsResponse = await client.GetAsync("https://api.videoindexer.ai/auth/trial/Accounts");
            var jsonUriAccountsResponse = await uriAccountsResponse.Content.ReadAsStringAsync();
            dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(jsonUriAccountsResponse);
            var videoIndexerAccountId = accounts[0].id;
            model.AccountId = videoIndexerAccountId.ToString();

            var uriResponse = await client.GetAsync($"https://api.videoindexer.ai/auth/trial/Accounts/{videoIndexerAccountId}/Videos/{model.Video.VideoId}/AccessToken");
            var jsonUriResponse = await uriResponse.Content.ReadAsStringAsync();

            model.AccessToken = jsonUriResponse.Replace("\"", string.Empty);


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