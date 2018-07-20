using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Interpreter
{
    public class ParseException : Exception
    {
        public ParseException(Token t) : this("", t)
        {
        }

        public ParseException(string msg, Token t):base("syntax error around " + t.ToString() + ", " + msg)
        {
        }

        private static string location(Token t)
        {
            if (t == Token.EOF)
            {
                return "the last line";
            }
            else
            {
                return "\"" + t.Text + "\" at line " + t.LineNumber;
            }
        }

        public ParseException(IOException e) : base(e.Message+"\n"+e.StackTrace)
        {

        }
        public ParseException(string msg) : base(msg) { }
    }
    public class StoneException:Exception
    {
        public StoneException(string m) : base(m) { }

        public StoneException(string m, StackTrace t) : base(m + " " + t.ToString())
        {
        }
    }
}