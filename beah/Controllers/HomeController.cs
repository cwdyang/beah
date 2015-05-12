using System.Web.Mvc;
using beah.Library.Web.REST;

namespace beah.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRestHelper _restHelper;

        public HomeController()
        {
            
        }

        public HomeController(IRestHelper restHelper)
        {
            _restHelper = restHelper;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            _restHelper.CreateRequest("", "POST", 6000,"");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
