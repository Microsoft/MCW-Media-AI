using ContosoLearning.Data;
using System.Collections.Generic;
using System.Linq;

namespace ContosoLearning.Web.Public.Models
{
    public class HomeIndexModel
    {
        public IEnumerable<VideoListModel> Videos { get; set; }

        public bool HasVideos
        {
            get
            {
                return this.Videos != null && this.Videos.Count() > 0;
            }
        }
    }
}