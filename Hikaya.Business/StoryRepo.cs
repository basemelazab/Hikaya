using Hikaya.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Hikaya;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Hikaya.Business
{
     public class StoryRepo : IStoryRepo
     {
        public void Add(Story story)
        {
            using (HikayaDBContext db = new HikayaDBContext())
            {
               
                try
                {
                    db.Stories.Add(story);
                    db.SaveChanges();

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
            using (HikayaDBContext db = new HikayaDBContext())
            {
                Story story = db.Stories.Find(id);
                db.Stories.Remove(story);
                db.SaveChanges();
            }
        }



        public List<Story> GetAllStories()
        {
            HikayaDBContext db = new HikayaDBContext();

            List<Story> story = db.Stories.ToList();

            return story;


        }

        public Story GetStoryById(int id)
        {
            using (HikayaDBContext db = new HikayaDBContext())
            {
                Story story = db.Stories
                    .Where(p => p.Id == id)
                    .Include(p => p.StoryPlots)
                    .FirstOrDefault();

                return story; ;
            }
        }

     

        public void Update(Story story)
        {
            using (HikayaDBContext db = new HikayaDBContext())
            {
                Story oldstory = db.Stories.Find(story.Id);
                db.Entry(oldstory).CurrentValues.SetValues(story);
                db.SaveChanges();
            }
        }
       
    }
}
