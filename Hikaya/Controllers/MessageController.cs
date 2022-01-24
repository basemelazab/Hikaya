using Hikaya.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hikaya.Business.Interfaces;
using Hikaya.DAL;
using Hikaya.Helpers;
using Hikaya.Resources;

namespace Hikaya.Controllers
{
    public class MessageController : Controller
    {
        public readonly IStoryRepo storyRepo;
        public IFeedBackMessages feedBackMessageRepo;
        public MessageController(IStoryRepo storyRepo, IFeedBackMessages feedBackMessageRepo)
        {
            this.storyRepo = storyRepo;
            this.feedBackMessageRepo = feedBackMessageRepo;
        }
        // GET: Message
        public ActionResult Send(int id)
        {
            FeedbackMessage feedbackMessage = new FeedbackMessage();
            feedbackMessage.StoryId = id;
            feedbackMessage.Story = storyRepo.GetStoryById(id);

            return View(feedbackMessage);
        }

       
        [HttpPost]
        public ActionResult Send(FeedbackMessage model)
        {

            model.Date = DateTime.Now;
            model.UserId = UserHelper.GetCurrentUserId();
            feedBackMessageRepo.Add(model);
            TempData["SuccessMessage"] = StoryResourcesArabic.FeedBackSendMessage;
            return RedirectToAction("Index","Home");
        }

        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Inbox()
        {
            var model = feedBackMessageRepo.GetAllFeedbackMessages(UserHelper.GetCurrentUserId());
            return View(model);
        }
    }
}
