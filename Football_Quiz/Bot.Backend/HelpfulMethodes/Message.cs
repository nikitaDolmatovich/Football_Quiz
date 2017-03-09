using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bot.Backend.Resources;
using Bot.Backend.Models;

namespace Bot.Backend.HelpfulMethodes
{
    public static class Message
    {
        private static string str = "";

        public static string GetWelcomeMessage()
        {

            var helloString = "Добро пожаловать в футбольную викторину!\n" +
                      $"\n/start - {ResourceBot.MainMenu}\n" +
                      $"\n/play - {ResourceBot.StartGame}\n" +
                      $"\n/thematic - {ResourceBot.ThematicGame}\n" +
                      $"\n/stat - {ResourceBot.Statistics}";

            return helloString;
        }

        public static string ShowQuestion(string question, List<string> answers, Question obj)
        {
            var questionString = question + "?\n" +
                "\nA)" + answers[0] + "\n" +
                "\nB)" + answers[1] + "\n" +
                "\nC)" + answers[2] + "\n" +
                "\nD)" + answers[3] + "\n" +
                "\nЭтот вопрос стоит " + obj.Raiting + " Очков";

            return questionString;
        }

        public static List<string> GetAnswers()
        {
            List<string> answers = new List<string>();

            for(int i = 0; i < str.Length - 1; i++)
            {
                if(Char.IsNumber(str[i]) && Char.IsNumber(str[i+1]))
                {
                    int test = str[i] + str[i + 1];
                    answers.Add(test.ToString());
                }
            }

            return answers;
        }
    }
}
