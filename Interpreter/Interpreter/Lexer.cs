using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Interpreter
{
    public class Lexer
    {
        public static string regexPat2 = @"\s*((//.*)|([0-9]+)|(""(\\""|\\\\|\\n|[^""])*"")"
                                        + @"|[A-Z_a-z][A-Z_a-z0-9]*|==|<=|>=|&&|\|\||\P{P}|\{|\})?";
        public static string regexPat = "\\s*((//.*)|([0-9]+)|(\"(\\\\\"|\\\\\\\\|\\\\n|[^\"])*\")"
            + "|[A-Z_a-z][A-Z_a-z0-9]*|==|<=|>=|&&|\\|\\||\\p{P})?";
        private Regex regex=new Regex(regexPat2);
        private ArrayList<Token> queue = new ArrayList<Token>();
        private bool hasMore;
        private LineNumberReader reader;

        public Lexer(LineNumberReader r)
        {
            hasMore = true;
            reader=r;
            
        }

        public Token Read()
        {
            if (FillQueue(0))
                return queue.Remove(0);
            else
                return Token.EOF;
        }

        public Token Peek(int i)
        {
            if (FillQueue(i))
                return queue[i];
            else
                return Token.EOF;
        }

        private bool FillQueue(int i)
        {
            while (i >= queue.Count)
            {
                if (hasMore)
                    ReadLine();
                else
                {
                    return false;
                }
            }

            return true;
        }

        protected void ReadLine()
        {
            string line;
            try
            {
                line = reader.ReadLine();
            }
            catch (IOException e)
            {
                throw new ParseException(e.Message+"\n"+e.StackTrace);
            }

            if (line == null)
            {
                hasMore = false;
                return;
            }

            int lineNo = reader.LineNumber;
            Match matcher = regex.Match(line);
            int pos = 0;
            int endPos = line.Length;
            while (matcher.Success)
            {
                addToken(lineNo, matcher);

                ////matcher.region(pos, endPos);
                //if (matcher.lookingAt())
                //{
                //    pos = matcher.end();
                //}
                //else
                //{
                //    throw new ParseException("bad token at line " + lineNo);
                //}
                matcher = matcher.NextMatch();
            }

            queue.Add(new IdToken(lineNo, Token.EOL));
        }

        protected void addToken(int lineNo, Match matcher)
        {
            if (matcher.Groups[1].Success)
            {
                string m = matcher.Groups[1].ToString();
                if (!matcher.Groups[2].Success)
                {
                    Token token;
                    if(matcher.Groups[3].Success)
                        token=new NumToken(lineNo,int.Parse(m));
                    else if (matcher.Groups[4].Success)
                        token = new StrToken(lineNo, ToStringLiteral(m));
                    else
                        token = new IdToken(lineNo, m);
                    queue.Add(token);
                }
            }
        }

        protected string ToStringLiteral(string s)
        {
            StringBuilder sb = new StringBuilder();
            int len = s.Length - 1;
            for (int i = 1; i < len; i++)
            {
                char c = s[i];
                if (c == '\\' && i + 1 < len)
                {
                    int c2 = s[i + 1];
                    if (c2 == '"' || c2 == '\\')
                        c = s[++i];
                    else if (c2 == 'n')
                    {
                        ++i;
                        c = '\n';
                    }
                }

                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}