using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoLearning.Data
{
    public class VideoRepository : IVideoRepository
    {
        public VideoRepository(CosmosDbAuthInfo cosmosDbAuthInfo)
        {
            this._cosmosDbAuthInfo = cosmosDbAuthInfo;
        }

        private readonly CosmosDbAuthInfo _cosmosDbAuthInfo;

        public async Task<IEnumerable<Video>> GetAll()
        {
            var list = new List<Video>();

            // Code Here

            return list;
        }

        public async Task<Video> Get(string id)
        {
            // Code Here
            throw new NotImplementedException();
        }

        public async Task<Video> Insert(Video Video)
        {
            if (string.IsNullOrWhiteSpace(Video.Id))
            {
                throw new ArgumentNullException("Video", "Video.Id must have a value. It can not be null or empty.");
            }

            Video.Created = DateTime.UtcNow;

            // Code Here

            return Video;
        }

        public async Task Delete(string videoId)
        {
            // Code Here
        }
    }
}