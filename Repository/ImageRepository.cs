using MDW.Entity;
using MDW.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MDW.Repository
{
    public class ImageRepository : RepositoryBase, IImageRepository
    {
        public ImageRepository()
        {
        }

        public async Task<bool> ImageExists(string filename)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Image>().Exists(i => i.Name.Equals(filename));
            });

            return result;
        }

        public async Task<List<Image>> GetImages()
        {
            var result = new List<Image>();

            await Task.Run(() =>
            {
                foreach(var i in Database.GetCollection<Image>().FindAll())
                {
                    result.Add(new Image()
                    {
                        Name = i.Name,
                        Group = i.Group
                    });
                }
            });

            return result;
        }

        public async Task<Image> GetImage(string filename)
        {
            Image result = null;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Image>().FindOne(i => i.Name.Equals(filename));
            });

            return result;
        }

        public async Task<string> CreateImage(Image image)
        {
            var result = string.Empty;

            await Task.Run(() => 
            {
                Database.GetCollection<Image>().Insert(image.Name, image);

                result = image.Name;
            });

            return result;
        }

        public async Task<bool> DeleteImage(string filename)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Image>().Delete(i => i.Name.Equals(filename)) > 0;
            });

            return result;
        }
    }
}