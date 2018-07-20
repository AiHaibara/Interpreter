using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public interface IEnvironment
    {
        void put(string name, Object value);
        Object get(string name);
    }
}
