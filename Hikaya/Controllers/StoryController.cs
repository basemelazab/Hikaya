using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hikaya.Business;
using Hikaya.Business.Interfaces; 
using Hikaya.DAL;
using Hikaya.Helpers;
using Hikaya.Resources;

namespace Hikaya.Controllers
{

    public class StoryController : Controller
    {
        public readonly IStoryRepo storyRepo;
        public readonly ISavedStoryRepo SavedStoryRepo;
        public UserHelper userHelper;

        public StoryController(IStoryRepo storyRepo, ISavedStoryRepo SavedStoryRepo)
        {
            this.storyRepo = storyRepo;
            this.SavedStoryRepo = SavedStoryRepo;
        }
        // GET: Story
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Story story = storyRepo.GetStoryById(id);
            return View(story);
        }


        [HttpGet]
        public ActionResult Create()
        {
     
            Story story = new Story
            {
                StoryPlots = new List<StoryPlot>()
            };
            story.StoryPlots.Add(new StoryPlot());

            return View(story);
        }

        [HttpPost]

        public ActionResult Create(Story model, IEnumerable<HttpPostedFileBase> files)
        {
            model.PostDate = DateTime.Now;
            model.UserId = UserHelper.GetCurrentUserId();


            int i = 0;
            foreach (StoryPlot storyplot in model.StoryPlots)
            {
                if (files != null && files.Count() > i && files.ElementAt(i) != null)
                {
                    var file = files.ElementAt(i);
                    string image = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/ Images"), image);
                    file.SaveAs(path);
                    storyplot.ImageUrl = "~/Images/" + image;
                }
                i++;
            }
            StoryRepo storyRepo = new StoryRepo();
            storyRepo.Add(model);
            TempData["SuccessMessage"] = StoryResourcesArabic.StoryAddedMessage;
            
            return Redirect(Url.Content("~/"));
        }
        public ActionResult SavedStory(int id)
        {

            SavedStory savedStory = new SavedStory
            {
                StoryId = id,
                UserId = UserHelper.GetCurrentUserId(),
                Date = DateTime.Now
            };
            SavedStoryRepo.Add(savedStory);
            TempData["SuccessMessage"] = StoryResourcesArabic.StorySavedMessage;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeleteStory(int id)
        {
           
            SavedStoryRepo.Delete(id);
            TempData["SuccessMessage"] = StoryResourcesArabic.DeleteSavedStoryMessage;
            return RedirectToAction("SavedStories", "Story");
        }
        public ActionResult SavedStories()
        {
            var model = SavedStoryRepo.GetAllSavedStories(UserHelper.GetCurrentUserId().ToString());


            return View(model);
        }

    }
}
