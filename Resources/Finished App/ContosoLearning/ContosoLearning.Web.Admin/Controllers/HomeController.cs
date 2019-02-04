using ContosoLearning.Data;
using ContosoLearning.Web.Admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;

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

            var videoRepo = VideoRepositoryFactory.Create();

            var video = await videoRepo.Get(id);

            await videoRepo.Delete(id);


            // ======================================================================
            // Delete files from Blob Storage
            // ======================================================================

            // Load Connection String to Azure Storage Account
            var videoConnString = ConfigurationManager.ConnectionStrings["videostorage"].ConnectionString;
            if (string.IsNullOrWhiteSpace(videoConnString))
            {
                throw new Exception("The 'videostorage' Connection String is NOT set");
            }

            // Get reference to the Blob Container to upload to
            var storageAccount = CloudStorageAccount.Parse(videoConnString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to 'video' container
            var videoContainer = blobClient.GetContainerReference("video");
            await videoContainer.CreateIfNotExistsAsync();

            // Delete Video file from Blob Storage
            var videoBlob = videoContainer.GetBlockBlobReference(id);
            await videoBlob.DeleteAsync();


            // ======================================================================
            // Delete video from Video Indexer service
            // ======================================================================

            var videoIndexerLocation = "trial";
            var videoIndexerTokenCredentials = new Microsoft.Rest.TokenCredentials(
                    ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]
                );
            var videoIndexerAuthClient = new VideoIndexer.Authorization.AuthorizationClient(videoIndexerTokenCredentials);

            // Get Video Indexer Account Id
            var accountsResponse = await videoIndexerAuthClient.GetAccountsWithHttpMessagesAsync(videoIndexerLocation);
            dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(await accountsResponse.Response.Content.ReadAsStringAsync());
            var videoIndexerAccountId = accounts[0].id as string;

            // Get Video Indexer Access Token
            var accountAccessTokenResponse = await videoIndexerAuthClient.GetAccountAccessTokenWithHttpMessagesAsync(videoIndexerLocation, videoIndexerAccountId, true);
            var accountAccessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(await accountAccessTokenResponse.Response.Content.ReadAsStringAsync());

            // Delete Video from Video Indexer Account
            var videoIndexerClient = new VideoIndexer.Operations.OperationsClient(videoIndexerTokenCredentials);
            var response = await videoIndexerClient.DeleteVideoWithHttpMessagesAsync(videoIndexerLocation, videoIndexerAccountId, video.VideoId, accountAccessToken);

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

            // Load Connection String to Azure Storage Account
            var videoConnString = ConfigurationManager.ConnectionStrings["videostorage"].ConnectionString;
            if (string.IsNullOrWhiteSpace(videoConnString))
            {
                throw new Exception("The 'videostorage' Connection String is NOT set");
            }

            // Get reference to the Blob Container to upload to
            var storageAccount = CloudStorageAccount.Parse(videoConnString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("video");
            await container.CreateIfNotExistsAsync();

            // Upload Video file to Blob Storage
            var blob = container.GetBlockBlobReference(videoId);
            await blob.UploadFromStreamAsync(file.InputStream);


            // ======================================================================
            // Save Video info to Cosmos DB Database / Collection
            // ======================================================================

            // Save new Document to Cosmos DB for this Video
            var videoRepo = VideoRepositoryFactory.Create();
            var video = new Video();
            video.Id = videoId;
           // This is added in the function app at input video.VideoId = videoId;
            video.Title = model.Title;
            video.Description = model.Description;
            video.ProcessingState = "Processing";
            video.ProcessingProgress = "0%";
            await videoRepo.Insert(video);


            return RedirectToAction("Index");
        }
    }
}