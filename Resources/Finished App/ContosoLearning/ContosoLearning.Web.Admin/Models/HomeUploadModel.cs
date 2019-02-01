using System.ComponentModel.DataAnnotations;

namespace ContosoLearning.Web.Admin.Models
{
    public class HomeUploadModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}