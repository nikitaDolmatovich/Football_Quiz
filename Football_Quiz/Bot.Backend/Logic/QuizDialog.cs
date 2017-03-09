﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Bot.Backend.Resources;
using Bot.Backend.HelpfulMethodes;
using Bot.Backend.Models;
using Microsoft.Bot.Builder.FormFlow;

namespace Bot.Backend.Logic
{
    [Serializable]
    public class QuizDialog : IDialog<object>
    {
        private Condition condition = new Condition();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> arguments)
        {
            var message = await arguments;
            var messageText = message.Text;

            switch(messageText)
            {
                case "/start":
                    condition.IsPlay = false;
                    var startMenu = Message.GetWelcomeMessage();
                    await context.PostAsync(startMenu); 
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/play":
                    var question = "Сколько команд в чемпионате Беларуси?";
                    await context.PostAsync(question);
                    var answer = message.Text;
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/thematic":
                    ChooseChampionat(context, ChoiceSelectAsync, "Выберите чемпионат");
                    break;
                default:
                    await context.PostAsync(CreateReply(message.Text));
                    context.Wait(MessageReceivedAsync);
                    break;      
            }
        }

        private string CreateReply(string answer)
        {
            if(answer == "16")
            {
                return "Ты прав!";
            }
            else
            {
                return "Ты дурак!";
            }
        }

        private void ChooseChampionat(IDialogContext context, ResumeAfter<string> method, string message)
        {
            BotContext botContext = new BotContext();
            ChampionatRepository repo = new ChampionatRepository(botContext);
            PromptDialog.Choice(context, method, repo.GetAll(), message);
        }

        private async Task ChoiceSelectAsync(IDialogContext context, IAwaitable<string> result)
        {
            var choice = await result;
            var question = new Questionnaire();

            condition.CurrentChampionat = choice;
            await context.PostAsync($"Чемпионат : {condition.CurrentChampionat}");
            await context.PostAsync(question.CreateChampionatReply());
            context.Wait(MessageReceivedAsync);
        }
    }
}