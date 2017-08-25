using MDW.Entity;
using MDW.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MDW.Repository
{
    public class PageRepository : RepositoryBase, IPageRepository
    {
        public PageRepository()
        {
        }

        public async Task<bool> PageExists(string url)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Page>().Exists(i => i.Url.Equals(url));
            });

            return result;
        }

        public async Task<List<Page>> GetPages()
        {
            var result = new List<Page>();

            await Task.Run(() =>
            {
                foreach(var i in Database.GetCollection<Page>().FindAll())
                {
                    result.Add(new Page()
                    {
                        Group = i.Group,
                        Name = i.Name,
                        Url = i.Url
                    });
                }
            });

            return result;
        }

        public async Task<Page> GetPageByUrl(string url)
        {
            Page result = null;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Page>().FindOne(i => i.Url.Equals(url));
            });

            return result;
        }

        public async Task<string> CreatePage(Page page)
        {
            var result = string.Empty;

            await Task.Run(() => 
            {
                Database.GetCollection<Page>().Insert(page.Url, page);
                result = page.Url;
            });

            return result;
        }

        public async Task<bool> UpdatePage(Page page)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Page>().Upsert(page.Url, page);
            });

            return result;
        }

        public async Task<bool> DeletePageByUrl(string url)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Page>().Delete(i => i.Url.Equals(url)) > 0;
            });

            return result;
        }
    }
}