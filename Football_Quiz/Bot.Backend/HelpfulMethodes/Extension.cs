﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bot.Backend.Resources;
using Bot.Backend.Models;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Text;

namespace Bot.Backend.HelpfulMethodes
{
    public static class Extension
    {
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

        public static string ShowRaiting(string position, int point, int count)
        {
            var raitingString = $"Ты занимешься {position} место!\n" +
                $"\nКоличество очков : {point}\n" +
                $"\nВсего участников : {count}\n";

            return raitingString;
        }

        public static IMessageActivity CreateButtons(IDialogContext context)
        {
            var card = new HeroCard("Варианты ответа");
            card.Buttons = new List<CardAction>()
            {
                new CardAction()
                {
                    Title = "A",
                    Type=ActionTypes.ImBack,
                    Value = "A",
                },
                new CardAction()
                {
                    Title = "B",
                    Type=ActionTypes.ImBack,
                    Value = "B"
                },
                new CardAction()
                {
                    Title = "C",
                    Type=ActionTypes.ImBack,
                    Value = "C"
                },
                new CardAction()
                {
                    Title = "D",
                    Type = ActionTypes.ImBack,
                    Value = "D"
                }
            };

            var reply = context.MakeMessage();
            reply.Attachments = new List<Attachment>();
            reply.Attachments.Add(new Attachment()
            {
                ContentType = HeroCard.ContentType,
                Content = card,
            });
            return reply;
        }

        public static string ParseQuestion(string question)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < question.Length; i++)
            {
                if ((char)question[i] != '?')
                {
                    sb.Append(question[i]);
                }
                else
                {
                    break;
                }
            }

            return sb.ToString();
        }

        public static string ParseVariant(string variant, string question)
        {
            StringBuilder sb = new StringBuilder();
            var symbol = Convert.ToChar(variant);

            for (int i = 0; i < question.Length; i++)
            {
                if (char.ToLower(question[i]) == char.ToLower(symbol) &&
                    question[i + 1] == ')')
                {
                    for (int j = i + 2; j < question.Length; j++)
                    {
                        if (question[j] != '\n')
                        {
                            sb.Append(question[j]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
