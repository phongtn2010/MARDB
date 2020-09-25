using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Web;

namespace DomainModel
{
    public static class Captcha
    {
        public static String generateRandomNumber(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            Char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(random.Next(9).ToString());
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static String generateRandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            Char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
