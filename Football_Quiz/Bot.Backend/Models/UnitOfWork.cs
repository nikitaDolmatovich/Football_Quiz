using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Models
{
    [Serializable]
    public class UnitOfWork : IDisposable
    {
        private BotContexts context = new BotContexts();
        private ChampionatRepository championatRepo;
        private QuestionRepository questionRepo;
        private UserRepository userRepo;
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

        public QuestionRepository Question
        {
            get
            {
                if(questionRepo == null)
                {
                    questionRepo = new QuestionRepository(context);
                }
                return questionRepo;
            }
        }

        public UserRepository User
        {
            get
            {
                if(userRepo == null)
                {
                    userRepo = new UserRepository(context);
                }

                return userRepo;
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