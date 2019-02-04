using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoLearning.Data
{
    public class Video
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("processingState")]
        public string ProcessingState { get; set; }

        [JsonProperty("processingProgress")]
        public string ProcessingProgress { get; set; }

        public DateTime Created { get; set; }

        public bool IsProcessing()
        {
            return !string.IsNullOrWhiteSpace(this.ProcessingState) && !this.ProcessingState.Equals("Processed", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
