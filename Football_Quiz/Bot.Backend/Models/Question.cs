//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bot.Backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public int QuestionId { get; set; }
        public string QuestionValue { get; set; }
        public int Raiting { get; set; }
        public int ChampionatId { get; set; }
        public string AnswerTrue { get; set; }
        public string AnswerFalseFirst { get; set; }
        public string AnswerFalseSecond { get; set; }
        public string AnswerFalseThird { get; set; }
    
        public virtual Championat Championat { get; set; }
    }
}