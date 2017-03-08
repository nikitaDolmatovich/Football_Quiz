using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Backend.Models;

namespace Bot.Tests
{
    [TestClass]
    public class TestExceptions
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),
            "Null Reference exception")]
        public void TestThrowExceptionGetChampionat()
        {
            ChampionatRepository repo = new ChampionatRepository();

            repo.Get("qwerty");
        }
    }
}
