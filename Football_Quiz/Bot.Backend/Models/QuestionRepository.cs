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
            var championat = context.Championats.FirstOrDefault(x => x.ChampionatName == name);

            if(championat != null)
            {
                var question = context.Questions.FirstOrDefault(x => x.ChampionatId == championat.ChampionatId);

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
            else
            {
                NullReferenceException ex = new NullReferenceException("Null Reference exception");
                throw ex;
            }
        }

        public string GetNewRandomQuestion(string championatName)
        {
            var championat = context.Championats.FirstOrDefault(x => x.ChampionatName == championatName);

            if(championat != null)
            {
                var questions = context.Questions.Where(x => x.ChampionatId == championat.ChampionatId).ToList();

                if (questions != null)
                {
                    return questions[Random.Next(0, questions.Count)].QuestionValue;
                }
                else
                {
                    NullReferenceException ex = new NullReferenceException("Null reference exception");
                    throw ex;
                }
            }
            else
            {
                NullReferenceException ex = new NullReferenceException("Null reference expection");
                throw ex;
            }
        }

        public string GetRandomQuestion()
        {
            var question = context.Questions.ToList();

            return question[Random.Next(0, question.Count)].QuestionValue;
        }
    }
}