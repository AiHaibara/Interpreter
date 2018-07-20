namespace Interpreter
{
    public class Precedence
    {
        public int value;
        public bool leftAssoc; // left associative
        public Precedence(int v, bool a)
        {
            value = v; leftAssoc = a;
        }
    }
}