using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bot.Backend.Resources;

namespace Bot.Backend.HelpfulMethodes
{
    public static class Message
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
    }
}
