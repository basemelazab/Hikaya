using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hikaya.Business.Interfaces;
using Hikaya.DAL;

using System.Data.Entity;

namespace Hikaya.Business
{
    public class FeedBackMessageRepo : IFeedBackMessages
    {
        public void Add(FeedbackMessage feedbackMessage)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                context.FeedbackMessages.Add(feedbackMessage);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                FeedbackMessage feedbackMessage = context.FeedbackMessages.Find(id);
                context.FeedbackMessages.Remove(feedbackMessage);
                context.SaveChanges();
            }
        }

        public List<FeedbackMessage> GetAllFeedbackMessages(string userId)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                List<FeedbackMessage> feedbackMessages = context.FeedbackMessages.Include(p => p.Story).Where(fm => fm.Story.UserId == userId).ToList();
                return feedbackMessages;
            }
        }

        public FeedbackMessage GetFeedbackMessageById(int id)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                FeedbackMessage feedbackMessage = context.FeedbackMessages.Find(id);
                return feedbackMessage;
            }
        }

        public void Update(FeedbackMessage feedbackMessage)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                FeedbackMessage oldFeedbackMessage = context.FeedbackMessages.Find(feedbackMessage.Id);
                context.Entry(oldFeedbackMessage).CurrentValues.SetValues(feedbackMessage);
                context.SaveChanges();
            }
        }
    }
}
    

  

       
