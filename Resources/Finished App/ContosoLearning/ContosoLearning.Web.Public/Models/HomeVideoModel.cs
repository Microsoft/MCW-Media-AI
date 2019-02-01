using ContosoLearning.Data;

namespace ContosoLearning.Web.Public.Models
{
    public class HomeVideoModel
    {
        public Video Video { get; set; }
        public string ThumbnailUrl { get; set; }
		public string AccessToken { get; set; }
        public string AccountId { get; set; }
    }
}