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
using System.Text;
using System.Activities;

namespace Bot.Backend.Logic
{
    [Serializable]
    public class QuizDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> arguments)
        {
            Singletone singletone = Singletone.Instance;
            BotContexts botContext = new BotContexts();
            UserRepository repo = new UserRepository(botContext);
            var message = await arguments;
            var messageText = message.Text;
            var quest = new Questionnaire();

            switch(messageText)
            {
                case "/start":
                    var startMenu = Extension.GetWelcomeMessage();
                    await context.PostAsync(startMenu);
                    if (!repo.IsExist(context.MakeMessage().Recipient.Name))
                    {
                        if (context.MakeMessage().Recipient.Name == null)
                        {
                            await context.PostAsync("Установить в настройках username, иначе я не смогу сохранить ваш рейтинг!");
                        }
                        else
                        {
                            repo.AddUser(context.MakeMessage().Recipient.Name);
                        }
                    }
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/play":
                    if(!repo.IsExist(context.MakeMessage().Recipient.Name))
                    {
                        await context.PostAsync("Установить в настройках username, иначе я не смогу сохранить ваш рейтинг!");
                    }
                    else
                    {
                        await context.PostAsync(quest.CreateRandomQuetion());
                        await context.PostAsync(CreateButtons(context));
                    }
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/thematic":
                    if (!repo.IsExist(context.MakeMessage().Recipient.Name))
                    {
                        await context.PostAsync("Установить в настройках username, иначе я не смогу сохранить ваш рейтинг!");
                    }
                    else
                    {
                        ChooseChampionat(context, ChoiceSelectChampionatAsync, "Выберите чемпионат : ");
                    }                
                    break;
                case "/stat":
                    var position = repo.GetPosition(context.MakeMessage().Recipient.Name).ToString();
                    var positionMessage = Extension.ShowRaiting(position, repo.GetCurrentRaiting(context.MakeMessage().Recipient.Name), repo.GetAll().Count);
                    await context.PostAsync(positionMessage);
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/news":
                    News news = new News();
                    string str = await news.ShowNews();
                    await context.PostAsync(str);
                    context.Wait(MessageReceivedAsync);
                    break;
                default:
                    var answer = singletone.Condition.CurrentMessage.ParseVariant(messageText);
                    await context.PostAsync(quest.CreateReply(answer, singletone.Condition.CurrentQuestion, context.MakeMessage().Recipient.Name));
                    await context.PostAsync(CreateButtons(context));
                    context.Wait(MessageReceivedAsync);
                    break;      
            }
        }

        private void ChooseChampionat(IDialogContext context, ResumeAfter<string> method, string message)
        {
            BotContexts botContext = new BotContexts();
            ChampionatRepository repo = new ChampionatRepository(botContext);
            PromptDialog.Choice(context, method, repo.GetAll(), message);
        }

        private async Task ChoiceSelectChampionatAsync(IDialogContext context, IAwaitable<string> result)
        {
            var choice = await result;
            var question = new Questionnaire();
            Singletone singletone = Singletone.Instance;

            singletone.Condition.CurrentChampionat = choice;
            var questionString = question.CreateChampionatQuestion(choice);
            singletone.Condition.CurrentQuestion = questionString.ParseQuestion();
            singletone.Condition.CurrentMessage = questionString;
            await context.PostAsync($"Чемпионат : {singletone.Condition.CurrentChampionat}");
            await context.PostAsync(singletone.Condition.CurrentMessage);
            await context.PostAsync(CreateButtons(context));
            context.Wait(MessageReceivedAsync);
        }

        private IMessageActivity CreateButtons(IDialogContext context)
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
    }
}