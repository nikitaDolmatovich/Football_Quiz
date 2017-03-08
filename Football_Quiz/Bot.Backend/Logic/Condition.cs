using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot.Backend.Logic
{
    [Serializable]
    public class Condition
    {
        public string CurrentChampionat { get; set; }
        public string CurrentQuestion { get; set; }
    }
}