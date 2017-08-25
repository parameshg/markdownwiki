using MDW.Entity;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IMarkdownService
    {
        Task<string> Convert(string text);

        Task<string> Convert(Page page);
    }
}