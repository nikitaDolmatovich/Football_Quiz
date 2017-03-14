using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Models
{
    [Serializable]
    public class ChampionatRepository : IRepository<Championat>
    {
        private BotContext context;
        private static Random random = new Random();

        public ChampionatRepository(BotContext context)
        {
            this.context = context;
        }

        public ChampionatRepository() { }

        public List<string> GetAll()
        {
            var championat = context.Championats.Select(x => x.ChampionatName).ToList<string>();
            return championat;
        }

        public Championat Get(string name)
        {
            var dbEntry = context.Championats.FirstOrDefault(x => x.ChampionatName == name);

            if(dbEntry != null)
            {
                return dbEntry;
            }
            else
            {
                NullReferenceException ex = new NullReferenceException("Null Reference exception");
                throw ex;
            }
        }

        public int GetRaitingChampionat(string championat)
        {
            var entry = context.Championats.FirstOrDefault(x => x.ChampionatName == championat);

            if(entry != null)
            {
                return entry.RaitingOfChampionat;
            }
            else
            {
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }
    }
}