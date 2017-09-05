using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IImageService : IService
    {
        Task<bool> ImageExists(string filename);

        Task<List<Image>> GetImages();

        Task<Image> GetImage(string filename);

        Task<string> CreateImage(string name, string group, byte[] data);

        Task<bool> DeleteImage(string filename);
    }
}