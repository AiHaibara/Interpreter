using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{

    internal class PrimaryExpr :ASTList
    {
        public PrimaryExpr(List<ASTree> c) : base(c) { }
        public static ASTree Create(List<ASTree> c)
        {
            return c.Count == 1 ? c[0] : new PrimaryExpr(c);
        }
    }

}
