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
        private Singletone singletone = Singletone.Instance;
        private BotContexts context;
        private QuestionRepository repo;
        private UserRepository userRepo;

        public Questionnaire()
        {
            context = new BotContexts();
            repo = new QuestionRepository(context);
            userRepo = new UserRepository(context);
        }

        public string CreateChampionatQuestion(string championatName)
        { 
            var question = repo.GetNewRandomQuestion(championatName);
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);

            return CreateMessageQuestion(question, GetListAnswers(entry), entry);
           
        }

        public string CreateRandomQuetion()
        {
            var question = repo.GetRandomQuestion();
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);
          
            return CreateMessageQuestion(question, GetListAnswers(entry), entry);
            
        }

        public string CreateReply(string variant, string question, string username)
        {
            var entry = context.Questions.FirstOrDefault(x => x.QuestionValue == question);
            var championat = context.Championats.FirstOrDefault(x => x.ChampionatId == entry.ChampionatId);
            var user = context.Users.FirstOrDefault(x => x.Username == username);
            int currentRaiting = user.Raiting;
            int raitingForQuestion = CalculateRaiting(championat.RaitingOfChampionat, entry.Raiting);

            if (entry != null && championat != null)
            {
                if(string.Compare(variant.ToLower(), entry.AnswerTrue.ToLower()) == 0)
                {
                    int raiting = currentRaiting + raitingForQuestion;
                    userRepo.UpdateRaiting(username,raiting);
                    singletone.Condition.CurrentChampionat = championat.ChampionatName;
                    return "Ты заработал " + raitingForQuestion + "очков\n" +
                       "\nСледующий вопрос\n" +
                       "\n" + CreateChampionatQuestion(singletone.Condition.CurrentChampionat);
                }
                else
                {
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

        private List<string> GetListAnswers(Question question)
        {
            List<string> list = new List<string>();
            Random ran = new Random();
            int number = ran.Next(1, 4);

            switch (number)
            {
                case 1:
                    list.Add(question.AnswerTrue);
                    list.Add(question.AnswerFalseSecond);
                    list.Add(question.AnswerFalseFirst);
                    list.Add(question.AnswerFalseThird);
                    break;
                case 2:
                    list.Add(question.AnswerFalseSecond);
                    list.Add(question.AnswerTrue);
                    list.Add(question.AnswerFalseThird);
                    list.Add(question.AnswerFalseFirst);
                    break;
                case 3:
                    list.Add(question.AnswerFalseFirst);
                    list.Add(question.AnswerFalseSecond);
                    list.Add(question.AnswerTrue);
                    list.Add(question.AnswerFalseThird);
                    break;
                case 4:
                    list.Add(question.AnswerFalseThird);
                    list.Add(question.AnswerFalseFirst);
                    list.Add(question.AnswerFalseSecond);
                    list.Add(question.AnswerTrue);
                    break;
            }

            return list;
        }

        private int CalculateRaiting(int raitingChampionat, int raitingQuestion)
        {
            return raitingChampionat * raitingQuestion;
        }

        private string CreateMessageQuestion(string question, List<string> answers, Question entry)
        {
            singletone.Condition.CurrentQuestion = question;
            singletone.Condition.CurrentMessage = Extension.ShowQuestion(question, GetListAnswers(entry), entry);

            return singletone.Condition.CurrentMessage;
        }

    }
}