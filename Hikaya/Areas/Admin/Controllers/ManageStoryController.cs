using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hikaya.Business;
using Hikaya.DAL;
using Hikaya.Business.Interfaces;
using Hikaya.Resources;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Hikaya.Areas.Admin.Controllers
{
    public class ManageStoryController : Controller
    {
        public readonly IStoryRepo storyRepo;

        public ManageStoryController(IStoryRepo storyRepo)
        {
            this.storyRepo = storyRepo;
        }

        //GET: Admin/ManageStory
       [HttpGet]
        public ActionResult Index()
        {     
                var stories = storyRepo.GetAllStories()
                    .Where(p => !p.IsPublished.HasValue || p.IsPublished.Value == false)
                    .OrderByDescending(p => p.PostDate)
                    .ToList();
                return View(stories); 
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Story story = storyRepo.GetStoryById(id);
            return View(story);
        }

        public ActionResult Publish(int id)
        {
            Story story = storyRepo.GetStoryById(id);
            story.IsPublished = true;
            storyRepo.Update(story);
            TempData["SuccessMessage"] = StoryResourcesArabic.storyPublishedMessage;
            //return View(story);
            return Redirect(Url.Content("~/"));
        }

    }
}