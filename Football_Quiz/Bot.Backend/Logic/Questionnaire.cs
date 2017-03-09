using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bot.Backend.Models;

namespace Bot.Backend.Logic
{
    public class Questionnaire
    {
        public string CreateChampionatQuestion(Championat championat)
        {
            BotContext context = new BotContext();
            QuestionRepository repo = new QuestionRepository(context);

            var question = 
        }
    }
}