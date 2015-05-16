using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using beah.Domain.Models;
using beah.Library.Web.FileSystem;
using beah.Library.Web.REST;
using beah.Library.Web.Strings;
using beah.WebAPI.ViewModels;

namespace beah.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private IRestHelper _restHelper;
        private const string MAIL_SENDGRID_EMAIL_URI = "mail:sendgridEmailUri";
        private const string MAIL_SENDGRID_API_KEY = "mail:sendgridAPIKey";
        private const string MAIL_SENDGRID_API_PASSWORD = "mail:sendgridAPIPassword";

        private static string _emailTemplate;
        private static string _emailHtmlTemplate;
        
        public HomeController()
        {
            
        }

        static HomeController()
        {
            _emailTemplate =
               FileHelper.ReadFileContent(System.Web.HttpContext.Current.Server.MapPath("~/templates") + "/jobMail.txt")
                   .Replace(System.Environment.NewLine, string.Empty);

            _emailHtmlTemplate =
                FileHelper.ReadFileContent(System.Web.HttpContext.Current.Server.MapPath("~/templates") + "/jobMail.htm");
            //.Replace(System.Environment.NewLine, string.Empty);
        }
          
        public HomeController(IRestHelper restHelper)
        {
            _restHelper = restHelper;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Log a job here.";

            

            var address = new Address()
            {
                StreetNumberName = "14 Laurel Street",
                City = "Auckland",
                Longtitude = "-36.8846615",
                Latitude = "174.7053566"
            };

            var beaOrg = new Party()
            {
                Id = Guid.NewGuid(),
                EntityName = "BEA Cooperation",
                PartyType = PartyType.Organisation
            };

            var assigner = new Party()
            {
                Id = Guid.NewGuid(),
                First = "Dave",
                Last = "Yang",
                PartyType = PartyType.Admin,
                Email = "davidy@datacom.co.nz",
                ParentBaseObject = beaOrg
            };

            var propertyOp = new Party()
            {
                Id = Guid.NewGuid(),
                First = "Dave",
                Last = "Yang",
                PartyType = PartyType.Operator,
                Email = "davidy@datacom.co.nz",
                ParentBaseObject = beaOrg
            };

            var plumber = new Party()
            {
                Id = Guid.NewGuid(),
                First = "Steve",
                Last = "Seepe",
                PartyType = PartyType.Plumber,
                Email = "davidy@datacom.co.nz",
                ParentBaseObject = beaOrg
            };

            var builder = new Party()
            {
                Id = Guid.NewGuid(),
                First = "Tony",
                Last = "Lorne",
                PartyType = PartyType.Builder,
                Email = "davidy@datacom.co.nz",
                ParentBaseObject = beaOrg
            };

            var property = new Property()
            {
                Address = address,
                Id = Guid.NewGuid(),
                Operator = propertyOp,
                Instructions = "Do not enter buildilng after 4 PM"
            };

            var jobNew = new Job()
            {
                Properties = new List<Property>() { property },
                AssignedBy = assigner,
                Assignees = new List<Party>() { plumber, builder },
                Details = "Example details",
                Instructions = property.Instructions + "+additional instructions",
                PONumber = "PO0909",
                JobStatus = JobStatus.Logged,
                RequiredByDateTime = DateTime.Now.AddDays(3)
            };



            var jvm = new JobViewModel(jobNew);

            return View(jvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        /*
         JobViewModel job
        */
        [HttpPost]
        public ActionResult SendMail(beah.WebAPI.ViewModels.JobViewModel jvm)
        {
            ViewBag.Message = "Job Request Sent";


            var emailHtmlParams = new
            {
                description=jvm.Job.Details,
                instructions=jvm.Job.Instructions,
                address=jvm.Job.FullAddress,
                po=jvm.Job.PONumber,
                lat = Request.Params["Address.Latitude"],
                lng = Request.Params["Address.Longtitude"],
                location = jvm.Job.FullAddress
            };

            var emailTemplateParams = new
            {
                api_user=ConfigurationManager.AppSettings[MAIL_SENDGRID_API_KEY],
                api_key=ConfigurationManager.AppSettings[MAIL_SENDGRID_API_PASSWORD],
                to = "davidy@datacom.co.nz;" + Request.Params["Operator.Email"],
                toname=Server.UrlEncode("david yang"),
                subject=Server.UrlEncode("Spectrum Job PO #"),
                html=Server.UrlEncode(_emailHtmlTemplate.FormatWithNamedParameters(emailHtmlParams)),
                from="admin@bea.com"
            };

            var request = _restHelper.CreateRequest(ConfigurationManager.AppSettings[MAIL_SENDGRID_EMAIL_URI], RestHelper.HTTPPOST,
            _emailTemplate.FormatWithNamedParameters(emailTemplateParams), RestHelper.CONTENT_TYPE_FORM_URLENCODED
               ,null);

            var response = request.GetResponse();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
