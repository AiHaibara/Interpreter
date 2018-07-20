using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class Skip:Leaf
    {
        public Skip(string[] t) : base(t) { }
        protected override void Find(List<ASTree> res,Token t) { }
    }
}
