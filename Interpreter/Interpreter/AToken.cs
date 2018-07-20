using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal abstract class AToken : Element
    {
        protected Factory factory;
        protected AToken(Type type)
        {
            if (type == null)
                type = typeof(ASTLeaf);
            factory = Factory.Get(type,typeof(Token));
        }
        protected internal override void Parse(Lexer lexer, List<ASTree> res)
        {
            Token t = lexer.Read();
            if (Test(t)) {
                ASTree leaf = factory.Make(t);
                res.Add(leaf);
            }
            else
                throw new ParseException(t);
        }
        protected internal override bool Match(Lexer lexer)
        {
            return Test(lexer.Peek(0));
        }
        protected abstract bool Test(Token t); 
    }
}
