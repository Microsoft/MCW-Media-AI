using ContosoLearning.Data;
using System.Collections.Generic;
using System.Linq;

namespace ContosoLearning.Web.Admin.Models
{
    public class HomeIndexModel
    {
        public IEnumerable<Video> Videos { get; set; }

        public bool HasVideos
        {
            get
            {
                return this.Videos != null && this.Videos.Count() > 0;
            }
        }
    }
}