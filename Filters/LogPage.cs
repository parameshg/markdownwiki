using NLog;
using System.Web.Mvc;

namespace MDW.Filters
{
    public class LogPage : FilterAttribute, IActionFilter
    {
        private static Logger Log = LogManager.GetLogger("Markdown-Wiki");

        public void OnActionExecuted(ActionExecutedContext o)
        {
            Log.Info<string>(string.Format("[{0} | {1}] {2}", o.HttpContext.Request.Headers["Host"], o.HttpContext.Request.HttpMethod, o.HttpContext.Request.Url));
        }

        public void OnActionExecuting(ActionExecutingContext o)
        {
        }
    }
}