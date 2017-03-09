using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bot.Backend.Models;
using Bot.Backend.HelpfulMethodes;

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

            return Message.ShowQuestion(question, GetRandomAnswers(entry),entry);
        }

        private List<string> GetRandomAnswers(Question question)
        {
            List<string> list = GetListAnswers(question);
            Random rand = new Random();

            SwapList(ref list, rand.Next(0, NUMBER_QUESTION));

            return list;
        }

        private List<string> GetListAnswers(Question question)
        {
            List<string> list = new List<string>();

            list.Add(question.AnswerTrue);
            list.Add(question.AnswerFalseSecond);
            list.Add(question.AnswerFalseFirst);
            list.Add(question.AnswerFalseThird);

            return list;
        }

        private void SwapList(ref List<string> answers, int label)
        {
            int size = answers.Count;
            switch(label)
            {
                case 1:
                    break;
                case 2:
                    for(int i = 0; i < answers.Count - 2; i++)
                    {
                        answers[i] = answers[size];
                        size--;
                    }
                    break;
                case 3:
                    break;
                case 4:
                    for(int i = 0; i < answers.Count - 2; i++)
                    {
                        answers[i] = answers[size - 1];
                        size += 2;
                    }
                    break;
            }
        }
    }
}