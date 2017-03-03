using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Bot.Backend.Resources;
using Bot.Backend.HelpfulMethodes;

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

            switch(messageText)
            {
                case "/start":
                    var startMenu = Message.GetWelcomeMessage();
                    await context.PostAsync(startMenu); 
                    context.Wait(MessageReceivedAsync);
                    break;
            }
        }

    }
}