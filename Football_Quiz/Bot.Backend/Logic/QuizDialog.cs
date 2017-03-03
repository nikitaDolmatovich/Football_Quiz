using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Bot.Backend.Resources;

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
            var message = await arguments;
            var messageText = message.Text;
            //await context.PostAsync("You said : " + message.Text);
            //context.Wait(MessageReceivedAsync);

            switch(messageText)
            {
                case "/start":
                    var startMenu = GetStartMenu();
                    await context.PostAsync(startMenu); 
                    context.Wait(MessageReceivedAsync);
                    break;
            }
        }

        private string GetStartMenu()
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