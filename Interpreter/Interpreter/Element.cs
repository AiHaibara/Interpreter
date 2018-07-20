using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal abstract class Element
    {
        protected internal abstract void Parse(Lexer lexer, List<ASTree> res);
        protected internal abstract bool Match(Lexer lexer);
    }
}
