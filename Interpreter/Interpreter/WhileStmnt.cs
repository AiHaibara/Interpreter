using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class WhileStmnt : ASTList
    {
        public WhileStmnt(List<ASTree> list) : base(list)
        {
        }
        public ASTree condition() { return Child(0); }
        public ASTree body() { return Child(1); }
        
        public override string ToString()
        {
            return "(while " + condition() + " " + body() + ")";
        }
    }
}
