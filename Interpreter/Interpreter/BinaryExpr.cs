using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class BinaryExpr : ASTList
    {
        public BinaryExpr(List<ASTree> list) : base(list)
        {
        }
        public ASTree Left() { return Child(0); }
        public string Operator() { return ((ASTLeaf)Child(1)).token().Text; }
        public ASTree Right() { return Child(2); }
    }
}
