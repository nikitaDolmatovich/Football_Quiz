using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Logic
{
    [Serializable]
    public class Singletone
    {
        private static Singletone singletone;
        private Condition condition = new Condition();

        static Singletone()
        {
            singletone = new Singletone();
        }

        private Singletone() { }

        public static Singletone Instance
        {
            get { return singletone; }
        }

        public Condition Condition
        {
            get { return condition; }
        }
        
    }
}