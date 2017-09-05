using MDW.Filters;
using MDW.Models;
using MDW.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MDW.Controllers
{
    [LogPage]
    [LogError]
    [Authorize]
    public class MediaController : Controller
    {
        private IGroupService Groups { get; set; }

        private IImageService Image { get; set; }

        private IPolicyService Policies { get; set; }

        public MediaController(IImageService images, IGroupService groups, IPolicyService policies)
        {
            Image = images;
            Groups = groups;
            Policies = policies;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ActionResult result = null;

            var model = new MediaListModel();

            foreach (var i in await Groups.GetGroups())
            {
                model.Groups.Add(new GroupModel()
                {
                    Name = i.Name,
                    Builtin = i.Builtin
                });
            }

            foreach (var i in await Image.GetImages())
            {
                model.Images.Add(new ImageModel()
                {
                    Name = i.Name,
                    Group = i.Group
                });
            }

            result = View(model);

            return result;
        }

        [HttpGet]
        public async Task<ActionResult> Images(string id)
        {
            ActionResult result = null;

            if (await Policies.EvaluateImage((string)Session["Username"], id))
            {
                var o = await Image.GetImage(id);

                if (o != null && o.Data != null)
                    result = File(o.Data, "image/png");
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateImage([Bind(Include = "group")] CreateImageModel model)
        {
            ActionResult result = null;

            byte[] buffer = null;

            if (Request.Files.Count.Equals(1))
            {
                model.Name = Request.Files[0].FileName;

                buffer = new byte[Request.Files[0].ContentLength];

                using (var reader = new StreamReader(Request.Files[0].InputStream))
                {
                    reader.BaseStream.Read(buffer, 0, Request.Files[0].ContentLength);
                }
            }

            await Image.CreateImage(model.Name, model.Group, buffer);

            result = RedirectToAction("Index");

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteImage([Bind(Include = "name")] DeleteImageModel model)
        {
            ActionResult result = null;

            await Image.DeleteImage(model.Name);

            result = RedirectToAction("Index");

            return result;
        }
    }
}