using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class BasicEnv:IEnvironment
    {
        protected Dictionary<String, Object> values;
        public BasicEnv() { values = new Dictionary<String, Object>(); }
        public void put(String name, Object value) {
            if(!values.ContainsKey(name))
                values.Add(name, value);
            values[name] = value;
        }
        public Object get(String name) { return values[name]; }
    }
}
