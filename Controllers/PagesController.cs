using MDW.Entity;
using MDW.Filters;
using MDW.Models;
using MDW.Services;
using MDW.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MDW.Controllers
{
    [LogPage]
    [LogError]
    [Authorize]
    public class PagesController : Controller
    {
        private IPolicyService Policies { get; set; }

        private IPageService Pages { get; set; }

        private IGroupService Groups { get; set; }

        private IMarkdownService Markdown { get; set; }

        public PagesController(IPolicyService policies, IPageService pages, IGroupService groups, IMarkdownService markdown)
        {
            Pages = pages;
            Groups = groups;
            Markdown = markdown;
            Policies = policies;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ActionResult result = null;

            var model = new PageModel();

            model.Url = Request.Url.PathAndQuery;

            model.Authorized = await Policies.Evaluate((string)Session["Username"], Request.Url.PathAndQuery);
                
            if (model.Authorized)
            {
                foreach (var i in await Groups.GetGroups())
                {
                    model.Groups.Add(new GroupModel()
                    {
                        Name = i.Name,
                        Builtin = i.Builtin
                    });
                }

                var page = await Pages.GetPageByUrl(Request.Url.PathAndQuery);

                if (page != null)
                {
                    model.Url = page.Url;
                    model.Name = page.Name;
                    model.Group = page.Group;
                    model.Markdown = page.Body;
                    model.Html = await Markdown.Convert(page);
                }
            }
            else
            {
                model.Name = "Unauthorized";
                model.Markdown = "You're not authorized to view this page. Please contact your system administrator.";
                model.Html = await Markdown.Convert(model.Markdown);
            }

            result = View(model);

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "name, group")] CreatePageModel model)
        {
            ActionResult result = null;

            var url = await Pages.CreatePage(model.Name, model.Group);

            url = url.Replace("/", string.Empty);

            result = Redirect($"{Request.Url.Scheme}://{Request.Url.Host}:{Request.Url.Port}{url}");

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([Bind(Include = "url, name, group, body")] UpdatePageModel model)
        {
            ActionResult result = null;

            await Pages.UpdatePage(new Page()
            {
                Url = model.Url,
                Name = model.Name,
                Group = model.Group,
                Body = model.Body
            });

            result = Redirect(Request.UrlReferrer.ToString());
            
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "url")] DeletePageModel model)
        {
            ActionResult result = null;

            await Pages.DeletePageByUrl(model.Url);

            result = Redirect("/");

            return result;
        }
    }
}