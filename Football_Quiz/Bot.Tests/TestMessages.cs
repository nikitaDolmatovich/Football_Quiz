using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bot.Backend.Logic;
using Bot.Backend.Resources;
using Bot.Backend.HelpfulMethodes;

namespace Bot.Tests
{
    [TestClass]
    public class TestMessages
    {
        [TestMethod]
        public void TestWelcomeMessage()
        {
            var expected = "Добро пожаловать в футбольную викторину!\n" +
                     $"\n/start - {ResourceBot.MainMenu}\n" +
                     $"\n/play - {ResourceBot.StartGame}\n" +
                     $"\n/thematic - {ResourceBot.ThematicGame}\n" +
                     $"\n/stat - {ResourceBot.Statistics}";

            var actual = Message.GetWelcomeMessage();

            Assert.AreEqual<string>(expected, actual);
        }
    }
}
