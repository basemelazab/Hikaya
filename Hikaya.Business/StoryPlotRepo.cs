using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hikaya.DAL;
using Hikaya.Business.Interfaces;

namespace Hikaya.Business
{
    public class StoryPlotRepo : IStoryPlotRepo
    {
        
            public void Add(StoryPlot storyPlot)
            {
                using (HikayaDBContext context = new HikayaDBContext())
                {
                    context.StoryPlots.Add(storyPlot);
                    context.SaveChanges();
                }
            }

            public void Delete(int id)
            {
                using (HikayaDBContext context = new HikayaDBContext())
                {
                    StoryPlot storyPlot = context.StoryPlots.Find(id);
                    context.StoryPlots.Remove(storyPlot);
                    context.SaveChanges();
                }
            }

            public List<StoryPlot> GetAllStoryPlot(int storyId)
            {
                using (HikayaDBContext context = new HikayaDBContext())
                {
                    List<StoryPlot> storyPlotList = context.StoryPlots.Where(sp => sp.StoryId == storyId).ToList();
                    return storyPlotList;
                }
            }

       

        public StoryPlot GetStoryById(int id)
        {
            throw new NotImplementedException();
        }

        public StoryPlot GetStoryPlotById(int id)
            {
                using (HikayaDBContext context = new HikayaDBContext())
                {
                    StoryPlot storyPlot = context.StoryPlots.Find(id);
                    return storyPlot;
                }
        }

            public void Update(StoryPlot storyPlot)
            {
                using (HikayaDBContext context = new HikayaDBContext())
                {
                    StoryPlot oldStoryPlot = context.StoryPlots.Find(storyPlot.Id);
                    context.Entry(oldStoryPlot).CurrentValues.SetValues(storyPlot);
                    context.SaveChanges();
                }
            }
        }
    }

       