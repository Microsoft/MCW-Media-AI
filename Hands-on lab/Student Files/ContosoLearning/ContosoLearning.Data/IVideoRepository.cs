using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoLearning.Data
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAll();
        Task<Video> Get(string id);
        Task<Video> Insert(Video Video);
        Task Delete(string videoId);
    }
}
