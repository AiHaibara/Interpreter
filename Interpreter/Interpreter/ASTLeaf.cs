using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class ASTLeaf : ASTree
    {
        private static ArrayList<ASTree> empty = new ArrayList<ASTree>();
        protected Token Token;
        public ASTLeaf(Token t) { Token = t; }
        public override string ToString() { return Token.Text; }
        public Token token() { return Token; }

        public override ASTree Child(int i)
        {
            throw new IndexOutOfRangeException();
        }

        public override int NumChildren()
        {
            return 0;
        }

        public override IEnumerator<ASTree> ChildrenIt()
        {
            return empty.GetEnumerator();
        }

        public override string Location()
        {
            return "at line " + Token.LineNumber;
        }
    }
}
