using CommonMark;
using MDW.Entity;
using MDW.Services.Interfaces;
using System.Threading.Tasks;

namespace MDW.Services
{
    public class MarkdownService : IMarkdownService
    {
        public async Task<string> Convert(string text)
        {
            var result = string.Empty;

            await Task.Run(() => { result = CommonMarkConverter.Convert(text); });

            return result;
        }

        public async Task<string> Convert(Page page)
        {
            var result = string.Empty;

            await Task.Run(() => { result = CommonMarkConverter.Convert(page.Body); });

            return result;
        }
    }
}