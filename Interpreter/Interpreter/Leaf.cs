using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class Leaf:Element
    {
        protected String[] tokens;
        protected Leaf(String[] pat) { tokens = pat; }
        protected internal override void Parse(Lexer lexer, List<ASTree> res)
        {
            Token t = lexer.Read();
            if (t.IsIdentifier)
                foreach (String token in tokens)
                    if (token.Equals(t.Text)) {
                        Find(res, t);
                        return;
                    }

            if (tokens.Length > 0)
                throw new ParseException(tokens[0] + " expected.", t);
            else
                throw new ParseException(t);
        }
        protected virtual void Find(List<ASTree> res, Token t)
        {
            res.Add(new ASTLeaf(t));
        }
        protected internal override bool Match(Lexer lexer) 
        {
            Token t = lexer.Peek(0);
                    if (t.IsIdentifier)
                        foreach (String token in tokens)
                            if (token.Equals(t.Text))
                                return true;

                    return false;
        }
    }
}
