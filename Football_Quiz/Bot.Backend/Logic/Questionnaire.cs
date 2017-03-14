﻿using System;
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

        public string CreateReply(string variant, Condition condition, string username)
        {
            BotContext context = new BotContext();
            QuestionRepository repo = new QuestionRepository(context);
            UserRepository userRepo = new UserRepository(context);

            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == condition.CurrentQuestion);
            var championat = context.Championats.FirstOrDefault(x => x.ChampionatId == entry.ChampionatId);
            var user = context.Users.FirstOrDefault(x => x.Username == username);
            var currentRaiting = user.Raiting;

            if(entry != null && championat != null)
            {
                if(string.Compare(variant.ToLower(), entry.AnswerTrue.ToLower()) == 0)
                {
                    int? raiting = currentRaiting + CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting);
                    userRepo.UpdateRaiting(username,raiting);
                    return "Ты заработал " + CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting) + "очков\n" +
                       "\nСледующий вопрос\n" +
                       "\n" + CreateChampionatQuestion(condition.CurrentChampionat);
                }
                else
                {
                    int? raiting = currentRaiting - CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting);
                    userRepo.UpdateRaiting(username, raiting);
                    return "Вы ошиблись!\n" +
                        "\n Я снял у вас " + CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting) + " очков!" + 
                        "\n" + CreateChampionatQuestion(condition.CurrentChampionat);
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