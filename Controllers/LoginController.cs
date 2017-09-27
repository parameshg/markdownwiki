using MDW.Filters;
using MDW.Models;
using MDW.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MDW.Controllers
{
    [LogPage]
    [LogError]
    public class LoginController : ControllerBase
    {
        private IUserService Users { get; set; }

        public LoginController(IUserService users)
        {
            Users = users;
        }

        public async Task<ActionResult> Index()
        {
            ActionResult result = null;

            await Task.Run(() =>
            {
                if (Session["username"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        result = Redirect(Request.QueryString["ReturnUrl"]);
                    else
                        result = RedirectToAction("Index", "Pages");
                }
                else
                    result = View(new Login());
            });

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate([Bind(Include = "username, password, remember")] Login model)
        {
            ActionResult result = null;

            try
            {
                if (await Users.Authenticate(model.Username, model.Password))
                {
                    var user = await Users.GetUser(model.Username);

                    var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, model.Username));

                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = model.Remember,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }, identity);

                    Response.SetCookie(new HttpCookie("Username", user.Username));
                    Response.SetCookie(new HttpCookie("FirstName", user.FirstName));
                    Response.SetCookie(new HttpCookie("LastName", user.LastName));
                    Response.SetCookie(new HttpCookie("Name", user.Name));
                    Response.SetCookie(new HttpCookie("Email", user.Email));
                    Response.SetCookie(new HttpCookie("Theme", user.Theme));
                    Response.SetCookie(new HttpCookie("Gravatar", user.Gravatar));

                    Session.Add("Username", user.Username);
                    Session.Add("FirstName", user.FirstName);
                    Session.Add("LastName", user.LastName);
                    Session.Add("Name", user.Name);
                    Session.Add("Email", user.Email);
                    Session.Add("Theme", user.Theme);
                    Session.Add("Gravatar", user.Gravatar);

                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        result = Redirect(Request.QueryString["ReturnUrl"]);
                    else
                        result = RedirectToAction("Index", "Pages");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The username or password is incorrect.");

                    result = RedirectToAction("Index", "Login");
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error occurred while login");

                result = View("Index", new Login());
            }

            return result;
        }
    }
}