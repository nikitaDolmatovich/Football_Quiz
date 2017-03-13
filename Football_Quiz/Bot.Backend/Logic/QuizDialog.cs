using System;
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
        private Condition condition = new Condition();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> arguments)
        {
            var message = await arguments;
            var messageText = message.Text;
            var quest = new Questionnaire();
            BotContext botContext = new BotContext();
            UserRepository repo = new UserRepository(botContext);

            switch(messageText)
            {
                case "/start":
                    condition.IsPlay = false;
                    var startMenu = Message.GetWelcomeMessage();
                    await context.PostAsync(startMenu); 
                    if(!repo.IsExist(context.MakeMessage().Recipient.Name))
                    {
                        repo.AddUser(context.MakeMessage().Recipient.Name);
                    }
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/play":
                    if (!condition.IsPlay)
                    {
                        condition.IsPlay = true;
                        await context.PostAsync(quest.CreateRandomQuetion());
                        await context.PostAsync(Message.CreateButtons(context));
                    }
                    else
                    {
                        await context.PostAsync("Вы уже играете!");
                    }
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/enough":
                    condition.IsPlay = false;
                    await context.PostAsync("Игрза закончилась и ты заработал 10 монет!");
                    context.Wait(MessageReceivedAsync);
                    break;
                case "/thematic":
                    ChooseChampionat(context, ChoiceSelectChampionatAsync, "Выберите чемпионат : ");
                    break;
                default:
                    var answer = ParseVariant(messageText, condition.CurrentMessage);
                    await context.PostAsync(quest.CreateReply(answer, condition));
                    await context.PostAsync(Message.CreateButtons(context));
                    context.Wait(MessageReceivedAsync);
                    break;      
            }
        }

        private void ChooseChampionat(IDialogContext context, ResumeAfter<string> method, string message)
        {
            BotContext botContext = new BotContext();
            ChampionatRepository repo = new ChampionatRepository(botContext);
            PromptDialog.Choice(context, method, repo.GetAll(), message);
        }

        private async Task ChoiceSelectChampionatAsync(IDialogContext context, IAwaitable<string> result)
        {
            var choice = await result;
            var question = new Questionnaire();

            condition.CurrentChampionat = choice;
            var questionString = question.CreateChampionatQuestion(choice);
            condition.CurrentQuestion = ParseQuestion(questionString);
            condition.CurrentMessage = questionString;
            await context.PostAsync($"Чемпионат : {condition.CurrentChampionat}");
            await context.PostAsync(question.CreateChampionatQuestion(choice));
            await context.PostAsync(Message.CreateButtons(context));
            context.Wait(MessageReceivedAsync);
        }

        private string ParseQuestion(string question)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < question.Length; i++)
            {
                if((char)question[i] != '?')
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

        private string ParseVariant(string variant, string question)
        {
            StringBuilder sb = new StringBuilder();
            var symbol = Convert.ToChar(variant);

            for(int i = 0; i < question.Length; i++)
            {
                if(char.ToLower(question[i]) == char.ToLower(symbol) &&
                    question[i + 1] == ')')
                {
                    for(int j = i + 2; j < question.Length; j++)
                    {
                        if(question[j] != '\n')
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