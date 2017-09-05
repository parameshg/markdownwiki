using MDW.Entity;
using MDW.Repository.Interfaces;
using MDW.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services
{
    public class ImageService : ServiceBase, IImageService
    {
        private IImageRepository Repository { get; set; }

        public ImageService(IImageRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> ImageExists(string filename)
        {
            var result = false;

            result = await Repository.ImageExists(filename);

            return result;
        }

        public async Task<List<Image>> GetImages()
        {
            var result = new List<Image>();

            result.AddRange(await Repository.GetImages());

            return result;
        }

        public async Task<Image> GetImage(string filename)
        {
            Image result = null;

            result = await Repository.GetImage(filename);

            return result;
        }

        public async Task<string> CreateImage(string name, string group, byte[] data)
        {
            var result = string.Empty;

            result = await Repository.CreateImage(new Image()
            {
                Name = name,
                Group = group,
                Data = data
            });

            return result;
        }

        public async Task<bool> DeleteImage(string filename)
        {
            var result = false;

            result = await Repository.DeleteImage(filename);

            return result;
        }
    }
}