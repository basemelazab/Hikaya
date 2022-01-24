using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hikaya.DAL;
using Hikaya.Business.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Hikaya.Business
{
    public class SavedStoryRepository : ISavedStoryRepo
    {
        public void Add(SavedStory savedStory)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                try
                {
                    context.SavedStories.Add(savedStory);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }

                }
            }
        }

        public void Delete(int id)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                SavedStory savedStory = context.SavedStories.Find(id);
                if (savedStory != null)
                {
                    context.SavedStories.Remove(savedStory);
                    context.SaveChanges();
                }
            }
        }

        public List<SavedStory> GetAllSavedStories(string userId)
        {
            HikayaDBContext context = new HikayaDBContext();

            var savedStories = context.SavedStories
                .Where(sp => sp.UserId == userId).Include(x => x.Story).ToList();

            return savedStories;
        }

        public SavedStory GetSavedStoryById(int id)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                SavedStory savedStory = context.SavedStories.Find(id);
                return savedStory;
            }
        }

        public void Update(SavedStory savedStory)
        {
            using (HikayaDBContext context = new HikayaDBContext())
            {
                SavedStory oldSavedStory = context.SavedStories.Find(savedStory.Id);
                context.Entry(oldSavedStory).CurrentValues.SetValues(savedStory);
                context.SaveChanges();
            }
        }
    }
}

