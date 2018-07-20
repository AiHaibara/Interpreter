using Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class Repeat : Element
    {
        protected Parser parser;
        protected bool onlyOnce;

        public Repeat(Parser parser, bool onlyOnce)
        {
            this.parser = parser;
            this.onlyOnce = onlyOnce;
        }

        protected internal override bool Match(Lexer lexer)
        {
            return parser.Match(lexer);
        }

        protected internal override void Parse(Lexer lexer, List<ASTree> res)
        {
            while (parser.Match(lexer))
            {
                ASTree t = parser.Parse(lexer);
                if (t.GetType() != typeof(ASTList) || t.NumChildren() > 0)
                    res.Add(t);
                if (onlyOnce)
                    break;
            }
        }
    }
}
