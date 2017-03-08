using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Models
{
    [Serializable]
    public class UnitOfWork : IDisposable
    {
        private BotContext context = new BotContext();
        private ChampionatRepository championatRepo;
        private bool disposed = false;

        public ChampionatRepository Championat
        {
            get
            {
                if (championatRepo == null)
                {
                    championatRepo = new ChampionatRepository(context);
                }
                return championatRepo;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}