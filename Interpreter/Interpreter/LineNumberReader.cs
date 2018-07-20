using System.IO;

namespace Interpreter
{
    public class LineNumberReader:StringReader
    {
        public int LineNumber { get; set; }
        public LineNumberReader(string s) : base(s)
        {
        }

        public override string ReadLine()
        {
            LineNumber++;
            return base.ReadLine();
        }
    }
}