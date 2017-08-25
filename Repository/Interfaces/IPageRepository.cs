using System.Threading.Tasks;
using MDW.Entity;
using System.Collections.Generic;

namespace MDW.Repository.Interfaces
{
    public interface IPageRepository : IRepository
    {
        Task<bool> PageExists(string url);

        Task<List<Page>> GetPages();

        Task<Page> GetPageByUrl(string url);

        Task<string> CreatePage(Page page);

        Task<bool> UpdatePage(Page page);

        Task<bool> DeletePageByUrl(string url);
    }
}