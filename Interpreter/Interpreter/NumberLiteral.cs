using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class NumberLiteral : ASTLeaf
    {
        public NumberLiteral(Token t) : base(t)
        {
        }
        public int Value { get { return Token.Number; } }
    }
}
