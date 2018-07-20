using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class NegativeExpr : ASTList
    {
        public NegativeExpr(List<ASTree> list) : base(list)
        {
        }
        public ASTree Operand() { return Child(0); }
        public override string ToString()
        {
            return "-" + Operand();
        }
    }
}
