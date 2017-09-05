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

            if (Session["username"] != null)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    result = Redirect(Request.QueryString["ReturnUrl"]);
                else
                    result = RedirectToAction("Index", "Pages");
            }
            else
                result = View(new Login());

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

                    Response.SetCookie(new HttpCookie("Username", user.Username?.ToString()));
                    Response.SetCookie(new HttpCookie("FirstName", user.FirstName?.ToString()));
                    Response.SetCookie(new HttpCookie("LastName", user.LastName?.ToString()));
                    Response.SetCookie(new HttpCookie("Name", user.Name?.ToString()));
                    Response.SetCookie(new HttpCookie("Email", user.Email?.ToString()));
                    Response.SetCookie(new HttpCookie("Theme", user?.Theme?.ToString()));
                    Response.SetCookie(new HttpCookie("Gravatar", user?.Gravatar?.ToString()));

                    Session.Add("Username", user.Username?.ToString());
                    Session.Add("FirstName", user.FirstName?.ToString());
                    Session.Add("LastName", user.LastName?.ToString());
                    Session.Add("Name", user.Name?.ToString());
                    Session.Add("Email", user.Email?.ToString());
                    Session.Add("Theme", user?.Theme?.ToString());
                    Session.Add("Gravatar", user?.Gravatar?.ToString());

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