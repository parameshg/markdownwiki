using MDW.Entity;
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
    public class ProfileController : ControllerBase
    {
        private IUserService Users { get; set; }

        public ProfileController(IUserService users)
        {
            Users = users;
        }

        public async Task<ActionResult> Index()
        {
            ActionResult result = null;

            var model = new UserModel();

            var user = await Users.GetUser(Request.Cookies["Username"].Value);

            if (user != null)
            {
                model.Enabled = user.Enabled;
                model.Name = user.Name;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Username = user.Username;
                model.Email = user.Email;
                model.Role = user.Role;
                model.Theme = user.Theme;
                model.Gravatar = user.Gravatar;

                Response.Cookies["Theme"].Value = user?.Theme;
                Session["Theme"] = user?.Theme;
            }

            result = View(model);

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateUserModel model)
        {
            ActionResult result = null;

            var user = await Users.GetUser(model.Username);

            if (user != null)
            {
                await Users.UpdateUser(new User()
                {
                    Enabled = user.Enabled,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Email = model.Email,
                    Role = model.Role,
                    Theme = model.Theme,
                    Gravatar = user.Gravatar
                });
            }

            result = Redirect(Request.UrlReferrer.ToString());

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            ActionResult result = null;

            if (model.NewPassword == model.ConfirmPassword)
            {
                var updated = await Users.ChangePassword(model.Username, model.OldPassword, model.NewPassword);

                if (!updated)
                    ModelState.AddModelError(string.Empty, "The username or the old password is incorrect.");
            }
            else
                ModelState.AddModelError(string.Empty, "The new password and confirm password does not match.");

            result = RedirectToAction("Index");

            return result;
        }
    }
}