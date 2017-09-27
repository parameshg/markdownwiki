using MDW.Filters;
using MDW.Models;
using MDW.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MDW.Controllers
{
    [LogPage]
    [LogError]
    [Authorize]
    public class AdminController : Controller
    {
        public IPageService Pages { get; set; }

        public IUserService Users { get; set; }

        public IRoleService Roles { get; set; }

        public IGroupService Groups { get; set; }

        public IPolicyService Policies { get; set; }

        public AdminController(IPageService pages, IUserService users, IRoleService roles, IGroupService groups, IPolicyService policies)
        {
            Pages = pages;
            Users = users;
            Roles = roles;
            Groups = groups;
            Policies = policies;
        }

        public ActionResult Index()
        {
            return View("Users");
        }

        [HttpGet]
        public async Task<ActionResult> Configure(string id)
        {
            ActionResult result = null;

            if (id.Equals("Users", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var model = new UserListModel();

                (await Users.GetUsers()).ForEach(i =>
                {
                    model.Users.Add(new UserModel()
                    {
                        Enabled = i.Enabled,
                        Name = i.Name,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        Username = i.Username,
                        Email = i.Email,
                        Role = i.Role,
                        Gravatar = i.Gravatar,
                        Theme = i.Theme
                    });
                });

                (await Roles.GetRoles()).ForEach(i =>
                {
                    model.Roles.Add(new RoleModel()
                    {
                        Name = i.Name,
                        Builtin = i.Builtin
                    });
                });

                result = View("Users", model);
            }

            if (id.Equals("Roles", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var model = new RoleListModel();

                (await Roles.GetRoles()).ForEach(i =>
                {
                    model.Roles.Add(new RoleModel()
                    {
                        Name = i.Name,
                        Builtin = i.Builtin
                    });
                });

                result = View("Roles", model);
            }

            if (id.Equals("Pages", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var model = new PageListModel();

                (await Pages.GetPages()).ForEach(i =>
                {
                    model.Pages.Add(new PageModel()
                    {
                        Group = i.Group,
                        Name = i.Name,
                        Url = i.Url,
                        AbsoluteUrl = string.Format("{0}://{1}:{2}{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, i.Url.Replace("/", "/?"))
                    });
                });

                result = View("Pages", model);
            }

            if (id.Equals("Groups", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var model = new GroupListModel();

                (await Groups.GetGroups()).ForEach(i =>
                        {
                            model.Groups.Add(new GroupModel()
                            {
                                Name = i.Name,
                                Builtin = i.Builtin
                            });
                        });

                result = View("Groups", model);
            }

            if (id.Equals("Authorization", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var model = new PolicyListModel();

                (await Roles.GetRoles()).ForEach(i =>
                            {
                                model.Roles.Add(new RoleModel()
                                {
                                    Name = i.Name,
                                    Builtin = i.Builtin
                                });
                            });

                (await Groups.GetGroups()).ForEach(i =>
                {
                    model.Groups.Add(new GroupModel()
                    {
                        Name = i.Name,
                        Builtin = i.Builtin
                    });
                });

                (await Policies.GetPolicies()).ForEach(i =>
                {
                    model.Policies.Add(new PolicyModel()
                    {
                        Role = i.Role,
                        Group = i.Group,
                        Effect = i.Effect ? "ALLOW" : "DENY"
                    });
                });

                result = View("Authorization", model);
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser([Bind(Include = "firstname, lastname, username, email, role")] CreateUserModel model)
        {
            ActionResult result = null;

            await Users.CreateUser(model.FirstName, model.LastName, model.Username, model.Email, model.Role);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser([Bind(Include = "username")] DeleteUserModel model)
        {
            ActionResult result = null;

            await Users.DeleteUser(model.Username);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole([Bind(Include = "name")] CreateRoleModel model)
        {
            ActionResult result = null;

            await Roles.CreateRole(model.Name);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRole([Bind(Include = "name")] DeleteRoleModel model)
        {
            ActionResult result = null;

            await Roles.DeleteRole(model.Name);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGroup([Bind(Include = "name")] CreateGroupModel model)
        {
            ActionResult result = null;

            await Groups.CreateGroup(model.Name);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteGroup([Bind(Include = "name")] DeleteGroupModel model)
        {
            ActionResult result = null;

            await Groups.DeleteGroup(model.Name);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePage([Bind(Include = "url")] DeletePageModel model)
        {
            ActionResult result = null;

            await Pages.DeletePageByUrl(model.Url);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePolicy([Bind(Include = "role, group")] CreatePolicyModel model)
        {
            ActionResult result = null;

            await Policies.CreatePolicy(model.Role, model.Group, model.Effect);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePolicy([Bind(Include = "role, group")] DeletePolicyModel model)
        {
            ActionResult result = null;

            await Policies.DeletePolicy(model.Role, model.Group);

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }
    }
}