using MDW.Entity;
using MDW.Repository.Interfaces;
using MDW.Services.Interfaces;
using MDW.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services
{
    public class PageService : IPageService
    {
        private IPageRepository Repository { get; set; }

        public PageService(IPageRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<Page>> GetPages()
        {
            var result = new List<Page>();

            result.AddRange(await Repository.GetPages());

            return result;
        }

        public async Task<bool> PageExists(string url)
        {
            var result = false;

            url = url.Replace("?", string.Empty);

            result = await Repository.PageExists(url);

            return result;
        }

        public async Task<Page> GetPageByUrl(string url)
        {
            Page result = null;

            url = url.Replace("?", string.Empty);

            result = await Repository.GetPageByUrl(url) as Page;

            return result;
        }

        public async Task<string> CreatePage(string name, string group)
        {
            var result = string.Empty;

            var url = "/" + name.ToLower().Replace(" ", "-");
            var body = Ipsum.GetPhrase(500);

            name = string.IsNullOrEmpty(name) ? "Untitled Page" : name;
            group = string.IsNullOrEmpty(group) ? "default" : group;

            var page = new Page()
            {
                Name = name,
                Group = group,
                Body = body,
                Url = url
            };

            result = await Repository.CreatePage(page);

            return result;
        }

        public async Task<bool> UpdatePage(string url, string name, string group, string body)
        {
            var result = false;

            url = url.Replace("?", string.Empty);

            var page = new Page()
            {
                Url = url,
                Name = name,
                Group = group,
                Body = body
            };

            result = await Repository.UpdatePage(page);

            return result;
        }

        public async Task<bool> DeletePageByUrl(string url)
        {
            var result = false;

            url = url.Replace("?", string.Empty);

            result = await Repository.DeletePageByUrl(url);

            return result;
        }
    }
}