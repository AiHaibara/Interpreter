using System.Collections;
using System.Collections.Generic;

namespace Interpreter
{
    internal abstract partial class ASTree:IEnumerable<ASTree>
    {
        public abstract ASTree Child(int i);

        public abstract int NumChildren();

        public IEnumerator<ASTree> GetEnumerator()
        {
            return ChildrenIt();
        }

        public abstract IEnumerator<ASTree> ChildrenIt();

        public abstract string Location();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}