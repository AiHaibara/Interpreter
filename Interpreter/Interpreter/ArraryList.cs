using System.Collections.Generic;

namespace Interpreter
{
    public class ArrayList<T>:List<T>
    {
        public T Remove(int x)
        {
            var data = this[x];
            RemoveAt(x);
            return data;
        }
    }
}