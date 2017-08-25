using NLog;
using System.Web.Mvc;

namespace MDW.Controllers
{
    public class ControllerBase : Controller
    {
        protected Logger Log { get; private set; }

        public ControllerBase()
        {
            Log = LogManager.GetLogger("Markdown-Wiki");
        }
    }
}