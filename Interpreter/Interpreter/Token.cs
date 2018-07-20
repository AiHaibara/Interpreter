using System;
using System.Runtime.CompilerServices;

namespace Interpreter
{
    public class Token
    {
        protected Token(int line)
        {
            lineNumber = line;
        }
        public static Token EOF { get; set; } = new Token(-1);
        public static string EOL = "\\n"; 
        private int lineNumber;
        public int LineNumber { get; set; }

        public virtual bool IsIdentifier
        {
            get { return false; }
        }

        public virtual bool IsNumber
        {
            get { return false; }
        }

        public virtual bool IsString
        {
            get { return false; }
        }

        public virtual string Text
        {
            get { return ""; }
        }

        public virtual int Number
        {
            get
            {
                throw new NotImplementedException();
                return int.Parse(Text);
            }
        }
    }

    public class IdToken : Token
    {
        private string text;

        public IdToken(int line, string id):base(line)
        {
            text = id;
        }

        public override bool IsIdentifier
        {
            get { return true; }
        }
        public override string Text
        {
            get { return text; }
        }
    }

    public class NumToken : Token
    {
        private int value;

        public NumToken(int line, int v) : base(line)
        {
            value = v;
        }

        public override bool IsNumber
        {
            get { return true; }
        }

        public override string Text
        {
            get { return value.ToString(); }
        }

        public override int Number
        {
            get { return value; }
        }
    }

    public class StrToken : Token
    {
        private string literal;

        public StrToken(int line, string str) : base(line)
        {
            literal = str;
        }

        public override bool IsString
        {
            get { return true; }
        }

        public override string Text
        {
            get { return literal; }
        }
    }
}