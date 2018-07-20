using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class BlockStmnt : ASTList
    {
        public BlockStmnt(List<ASTree> list) : base(list)
        {
        }
    }
}
