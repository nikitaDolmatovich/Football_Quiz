using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Models
{
    public class ChampionatRepository : IRepository<Championat>
    {
        private BotContext context;

        public ChampionatRepository(BotContext context)
        {
            this.context = context;
        }

        public IEnumerable<Championat> GetAll()
        {
            return context.Championats;
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
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }
    }
}