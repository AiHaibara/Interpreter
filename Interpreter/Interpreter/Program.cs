using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class LexerRunner
    {
        public void Start()
        {
            var s = File.ReadAllText("in.txt");
            //var s = Console.ReadLine();
            LineNumberReader reader=new LineNumberReader(s);
            Lexer l=new Lexer(reader);
            for (Token t; (t = l.Read()) != Token.EOF;)
                Console.WriteLine("=> " + t.Text);
        }
    }

    public class ParserRunner
    {
        public void Start()
        {
            var s=File.ReadAllText("in.txt");
            //var s = Console.ReadLine();
            LineNumberReader reader = new LineNumberReader(s);
            Lexer l = new Lexer(reader);
            BasicParser bp = new BasicParser();
            while (l.Peek(0) != Token.EOF)
            {
                ASTree ast = bp.Parse(l);
                Console.WriteLine("=> " + ast.ToString());
            }
        }
    }
    public class Runner
    {
        public void Start()
        {
            BasicParser bp = new BasicParser();
            BasicEnv env = new BasicEnv();
            var s = File.ReadAllText("in.txt");
            //var s = Console.ReadLine();
            LineNumberReader reader = new LineNumberReader(s);
            Lexer l = new Lexer(reader);
            while (l.Peek(0) != Token.EOF)
            {
                ASTree ast = bp.Parse(l);
                if(!(ast is NullStmnt))
                {
                    Object r = ((ASTree)ast).eval(env);
                    //Console.WriteLine("=> " + ast.ToString());
                    Console.WriteLine("=> " + r.ToString());
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Console.InputEncoding = Encoding.Unicode;
            //Console.OutputEncoding = Encoding.Unicode;
            //LexerRunner test =new LexerRunner();
            Runner test = new Runner();
            test.Start();
        }
    }
}
