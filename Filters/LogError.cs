using NLog;
using System.Configuration;
using System.Web.Mvc;

namespace MDW.Filters
{
    public class LogError : HandleErrorAttribute
    {
        private static Logger Log = LogManager.GetLogger("Markdown-Wiki");

        public override void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, context.HttpContext.Request.Url.ToString());

            context.ExceptionHandled = true;

            var ViewData = new ViewDataDictionary();

            ViewData.Add("Api", ConfigurationManager.AppSettings["vx.api.url"]);
            ViewData.Add("ErrorMessage", context.Exception.Message);
            ViewData.Add("ErrorStackTrace", context.Exception.StackTrace);

            context.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = ViewData,
            };
        }
    }
}