using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

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

            using (var documentClient = this.createDocumentClient())
            {
                var documentQuery = documentClient.CreateDocumentQuery<Video>(
                    UriFactory.CreateDocumentCollectionUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection)
                    ).AsDocumentQuery();

                while (documentQuery.HasMoreResults)
                {
                    list.AddRange(
                        await documentQuery.ExecuteNextAsync<Video>()
                        );
                }
            }

            return list;
        }

        public async Task<Video> Get(string id)
        {
            using (var documentClient = this.createDocumentClient())
            {
                var response = await documentClient.ReadDocumentAsync<Video>(
                    UriFactory.CreateDocumentUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection, id), new RequestOptions() { PartitionKey = new PartitionKey(id) }
                    );

                return response.Document;
            }
        }

        public async Task<Video> Insert(Video Video)
        {
            if (string.IsNullOrWhiteSpace(Video.Id))
            {
                throw new ArgumentNullException("Video", "Video.Id must have a value. It can not be null or empty.");
            }

            Video.Created = DateTime.UtcNow;

            using (var documentClient = this.createDocumentClient())
            {
                await documentClient.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection),
                    Video
                    );
            }

            return Video;
        }

        public async Task Delete(string videoId)
        {
            using (var documentClient = this.createDocumentClient())
            {
                await documentClient.DeleteDocumentAsync(
                    UriFactory.CreateDocumentUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection, videoId), new RequestOptions() { PartitionKey = new PartitionKey(videoId) }
                    );
            }
        }

        private DocumentClient createDocumentClient()
        {
            return new Microsoft.Azure.Documents.Client.DocumentClient(
                new Uri(_cosmosDbAuthInfo.Endpoint),
                _cosmosDbAuthInfo.AuthKey
                );
        }
    }
}