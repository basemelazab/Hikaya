using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hikaya.DAL;

namespace Hikaya.Business.Interfaces
{
    public interface IFeedBackMessages
    {
        
        void Add(FeedbackMessage feedbackMessage);
        void Update(FeedbackMessage feedbackMessage);
        void Delete(int id);
        List<FeedbackMessage> GetAllFeedbackMessages(string userId);
        FeedbackMessage GetFeedbackMessageById(int id);
    }
}

