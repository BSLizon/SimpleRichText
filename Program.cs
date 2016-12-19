using System;
using System.Collections;
using System.Collections.Generic;
using SimpleRichText;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            List<Token> ls = new Parser().Tokenize("$$<asdf$<中$>as>df国>");

            foreach( var item in ls)
            {
                Console.WriteLine(string.Format("{0}   {1}", item.type, item.val));
            }
        }
    }

}
