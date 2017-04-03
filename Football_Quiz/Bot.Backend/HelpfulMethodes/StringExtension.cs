using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Bot.Backend.HelpfulMethodes
{
    public static class StringExtension
    {
        public static string ParseQuestion(this string question)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < question.Length; i++)
            {
                if ((char)question[i] != '?')
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

        public static string ParseVariant(this string question, string variant)
        {
            StringBuilder sb = new StringBuilder();
            var symbol = Convert.ToChar(variant);

            for (int i = 0; i < question.Length; i++)
            {
                if (char.ToLower(question[i]) == char.ToLower(symbol) &&
                    question[i + 1] == ')')
                {
                    for (int j = i + 2; j < question.Length; j++)
                    {
                        if (question[j] != '\n')
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