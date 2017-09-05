using System.Threading.Tasks;
using MDW.Entity;
using System.Collections.Generic;

namespace MDW.Repository.Interfaces
{
    public interface IImageRepository : IRepository
    {
        Task<bool> ImageExists(string filename);

        Task<List<Image>> GetImages();

        Task<Image> GetImage(string filename);

        Task<string> CreateImage(Image image);

        Task<bool> DeleteImage(string filename);
    }
}