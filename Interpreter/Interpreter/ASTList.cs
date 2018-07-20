using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal partial class ASTList : ASTree 
    {
        protected List<ASTree> Children;
        public ASTList(List<ASTree> list) { Children = list; }
        public override ASTree Child(int i) { return Children[i]; }
        public override int NumChildren() { return Children.Count; }
        public override IEnumerator<ASTree> ChildrenIt() { return Children.GetEnumerator(); }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('(');
            String sep = "";
            foreach (ASTree t in Children)
            {
                builder.Append(sep);
                sep = " ";
                builder.Append(t.ToString());
            }
            return builder.Append(')').ToString();
        }
        public override string Location()
        {
            foreach(ASTree t in Children)
            {
                String s = t.Location();
                if (s != null)
                    return s;
            }
            return null;
        }
    }
}
