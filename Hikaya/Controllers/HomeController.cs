using System.Web.Mvc;
using Hikaya.DAL;
using Hikaya.Business;
using Hikaya.Business.Interfaces;
using System.Linq;

namespace Hikaya.Controllers
{
    public class HomeController : Controller
    {
        public readonly IStoryRepo storyRepo;
        public HomeController(IStoryRepo storyRepo)
        {
            this.storyRepo = storyRepo;
        }

        public ActionResult Index()
        {
            var model = storyRepo.GetAllStories()
                .Where(p => p.IsPublished.HasValue && p.IsPublished.Value)
                .OrderByDescending(p => p.PostDate)
                .ToList();
            return View(model);
        }

        public ActionResult Search(string keywords)
        {
            var model = storyRepo
                .GetAllStories()
                .Where(p => p.Tags.Contains(keywords)
                || p.Title.Contains(keywords)).ToList();
            ViewBag.keywords = keywords;
            return View(model);
        }
    }
}