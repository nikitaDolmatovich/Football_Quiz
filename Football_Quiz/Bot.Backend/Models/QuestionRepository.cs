using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Models
{
    [Serializable]
    public class QuestionRepository
    {
        private BotContext context;
        private static  readonly Random Random = new Random();

        public QuestionRepository(BotContext context)
        {
            this.context = context;
        }

        public QuestionRepository() { }

        public List<string> GetAll()
        {
            var question = context.Questions.Select(x => x.QuestionValue).ToList<string>();
            return question;
        }

        public Question Get(string name)
        {
            var question = context.Questions.FirstOrDefault(x => x.QuestionValue == name);

            if(question != null)
            {
                return question;
            }
            else
            {
                NullReferenceException ex = new NullReferenceException("Null Reference exception");
                throw ex;
            }
        }

        public Question GetById(Championat championat)
        {
            var questions = context.Questions.Where(x => x.ChampionatId == championat.ChampionatId).ToList();

            if(questions != null)
            {
                return questions[Random.Next(0, questions.Count)];
            }
            else
            {
                NullReferenceException ex = new NullReferenceException("Null Reference exception");
                throw ex;
            }
        }
    }
}