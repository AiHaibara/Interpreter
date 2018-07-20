using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class StringLiteral : ASTLeaf
    {
        public StringLiteral(Token t) : base(t)
        {
        }
        public string Value { get { return Token.Text; } }
    }
}
