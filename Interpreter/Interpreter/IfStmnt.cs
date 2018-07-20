using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class IfStmnt : ASTList
    {
        public IfStmnt(List<ASTree> list) : base(list)
        {
        }
        public ASTree condition() { return Child(0); }
        public ASTree thenBlock() { return Child(1); }
        public ASTree elseBlock()
        {
            return NumChildren() > 2 ? Child(2) : null;
        }
        public override string ToString()
        {
            return "(if " + condition() + " " + thenBlock()
                     + " else " + elseBlock() + ")";
        }
    }
}
