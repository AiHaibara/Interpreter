using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class NullStmnt : ASTList
    {
        public NullStmnt(List<ASTree> list) : base(list)
        {
        }
    }
}
