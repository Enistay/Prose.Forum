using Prose.Application.Interfaces;
using System.Web.Mvc;

namespace Prose.UI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITopicService _topicService;
        public HomeController(ITopicService topicService)
        {
            _topicService = topicService;
        }
      
        public ActionResult Index(int page = 1)
        {
            ViewBag.PageNumber = page;
            ViewBag.PageAction = "Index";
            ViewBag.PageController = "Home";
            return View(_topicService.GetByPage(page));
        }

        public ActionResult Topic(int id)
        {           
            return View(_topicService.GetTopicById(id));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}