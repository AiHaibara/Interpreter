using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class OrTree:Element
    {
        protected Parser[] parsers;
        public OrTree(Parser[] p) { parsers = p; }
        protected internal override void Parse(Lexer lexer, List<ASTree> res)
        {
            Parser p = choose(lexer);
            if (p == null)
                throw new ParseException(lexer.Peek(0));
            else
                res.Add(p.Parse(lexer));
        }

        protected internal override bool Match(Lexer lexer)
        {
            return choose(lexer) != null;
        }
        /// <summary>
        /// 遍历ortree的各种parser找到与当前分词匹配parser
        /// </summary>
        /// <param name="lexer"></param>
        /// <returns></returns>
        protected Parser choose(Lexer lexer) 
        {
            foreach (Parser p in parsers)
                if (p.Match(lexer))
                {
                    return p;
                }
            return null;
        }
        protected void insert(Parser p)
        {
            Parser[] newParsers = new Parser[parsers.Length + 1];
            newParsers[0] = p;
            Array.Copy(parsers, 0, newParsers, 1, parsers.Length);
            parsers = newParsers;
        }
    }
}
