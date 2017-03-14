using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bot.Backend.Models;
using Bot.Backend.HelpfulMethodes;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace Bot.Backend.Logic
{
    public class Questionnaire
    {
        private const int NUMBER_QUESTION = 4;

        public string CreateChampionatQuestion(string championatName)
        {
            BotContext context = new BotContext();
            QuestionRepository repo = new QuestionRepository(context);

            var question = repo.GetNewRandomQuestion(championatName);
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);

            return Message.ShowQuestion(question, GetListAnswers(entry),entry);
        }

        public string CreateRandomQuetion()
        {
            BotContext context = new BotContext();
            QuestionRepository repo = new QuestionRepository(context);

            var question = repo.GetRandomQuestion();
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);

            return Message.ShowQuestion(question, GetListAnswers(entry), entry);
        }

        public string CreateReply(string variant, Condition condition)
        {
            BotContext context = new BotContext();
            QuestionRepository repo = new QuestionRepository();

            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == condition.CurrentQuestion);

            if(entry != null)
            {
                if(string.Compare(variant.ToLower(), entry.AnswerTrue.ToLower()) == 0)
                {

                    return "Ты заработал " + entry.Raiting + "очков\n" +
                       "\nСледующий вопрос\n" +
                       "\n" + CreateChampionatQuestion(condition.CurrentChampionat);
                }
                else
                {
                    return "Вы ошиблись, попробуйте снова!\n";
                }
            }
            else
            {
                NullReferenceException ex = new NullReferenceException();
                throw ex;
            }
        }

        public List<string> GetListAnswers(Question question)
        {
            List<string> list = new List<string>();

            list.Add(question.AnswerTrue);
            list.Add(question.AnswerFalseSecond);
            list.Add(question.AnswerFalseFirst);
            list.Add(question.AnswerFalseThird);

            return list;
        }
    }
}