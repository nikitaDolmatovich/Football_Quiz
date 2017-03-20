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
        private Singletone singletone = Singletone.Instance;

        public string CreateChampionatQuestion(string championatName)
        {
            BotContexts context = new BotContexts();
            QuestionRepository repo = new QuestionRepository(context);

            var question = repo.GetNewRandomQuestion(championatName);
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);
            singletone.Condition.CurrentQuestion = question;
            singletone.Condition.CurrentMessage = Extension.ShowQuestion(question, GetListAnswers(entry), entry);

            return singletone.Condition.CurrentMessage;
        }

        public string CreateRandomQuetion()
        {
            BotContexts context = new BotContexts();
            QuestionRepository repo = new QuestionRepository(context);

            var question = repo.GetRandomQuestion();
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);

            return Extension.ShowQuestion(question, GetListAnswers(entry), entry);
        }

        public string CreateReply(string variant, string question, string username)
        {
            BotContexts context = new BotContexts();
            QuestionRepository repo = new QuestionRepository(context);
            UserRepository userRepo = new UserRepository(context);

            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);
            var championat = context.Championats.FirstOrDefault(x => x.ChampionatId == entry.ChampionatId);
            var user = context.Users.FirstOrDefault(x => x.Username == username);
            var currentRaiting = user.Raiting;

            if(entry != null && championat != null)
            {
                if(string.Compare(variant.ToLower(), entry.AnswerTrue.ToLower()) == 0)
                {
                    int raitingForQuestion = CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting);
                    int raiting = currentRaiting + raitingForQuestion;
                    userRepo.UpdateRaiting(username,raiting);
                    singletone.Condition.CurrentChampionat = championat.ChampionatName;
                    return "Ты заработал " + raitingForQuestion + "очков\n" +
                       "\nСледующий вопрос\n" +
                       "\n" + CreateChampionatQuestion(singletone.Condition.CurrentChampionat);
                }
                else
                {
                    int raitingForQuestion = CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting);
                    int raiting = currentRaiting - raitingForQuestion;
                    userRepo.UpdateRaiting(username, raiting);
                    return "Вы ошиблись!\n" +
                        "\n Я снял у вас " +raitingForQuestion + " очков!" + 
                        "\n" + CreateChampionatQuestion(singletone.Condition.CurrentChampionat);
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

        public int CalculateRaiting(int raitingChampionat, int raitingQuestion)
        {
            return raitingChampionat * raitingQuestion;
        }

    }
}