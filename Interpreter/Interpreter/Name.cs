using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class Name : ASTLeaf
    {
        public Name(Token t) : base(t)
        {
        }
        public string NameValue { get { return Token.Text; } }
    }
}
