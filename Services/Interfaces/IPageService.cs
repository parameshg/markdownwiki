using System.Threading.Tasks;
using MDW.Entity;
using System.Collections.Generic;

namespace MDW.Services.Interfaces
{
    public interface IPageService
    {
        Task<bool> PageExists(string url);

        Task<List<Page>> GetPages();

        Task<Page> GetPageByUrl(string url);

        Task<string> CreatePage(string name, string group);

        Task<bool> UpdatePage(string url, string name, string group, string body);

        Task<bool> DeletePageByUrl(string url);
    }
}