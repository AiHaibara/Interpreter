using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class Operators : Dictionary<string,Precedence> {
        public static bool LEFT = true;
        public static bool RIGHT = false;
        public void Add(String name, int prec, bool leftAssoc)
        {
            Add(name, new Precedence(prec, leftAssoc));
        }
    }
}