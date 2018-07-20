using System;
using System.Collections.Generic;

namespace Interpreter
{
    internal class Parser
    {
        internal List<Element> elements;
        internal Factory factory;


        internal class Tree : Element
        {
            protected Parser parser;

            public Tree(Parser p)
            {
                parser = p;
            }

            protected internal override void Parse(Lexer lexer, List<ASTree> res)
            {
                res.Add(parser.Parse(lexer));
            }

            protected internal override bool Match(Lexer lexer)
            {
                return parser.Match(lexer);
            }
        }
        public Parser(Type t)
        {
            Reset(t);
        }
        public Parser Reset(Type t)
        {
            elements = new ArrayList<Element>();
            factory = Factory.GetForASTList(t);
            return this;
        }

        public static Parser rule() { return rule(null); }
        public static Parser rule(Type t)
        {
            return new Parser(t);
        }
        public Parser or(params Parser[] p)
        {
            elements.Add(new OrTree(p));
            return this;
        }
        public Parser sep(params string[] pat)
        {
            elements.Add(new Skip(pat));
            return this;
        }
        public Parser ast(Parser p)
        {
            elements.Add(new Tree(p));
            return this;
        }
        public Parser number(Type t)
        {
            elements.Add(new NumToken(t));
            return this;
        }
        public Parser identifier(Type t, HashSet<string> reserved)
        {
            elements.Add(new IdToken(t, reserved));
            return this;
        }
        public Parser expression(Type t,Parser subexp,Operators operators)
        {
            elements.Add(new Expr(t, subexp, operators));
            return this;
        }
        public Parser option(Parser p)
        {
            elements.Add(new Repeat(p, true));
            return this;
        }
        public Parser repeat(Parser p)
        {
            elements.Add(new Repeat(p, false));
            return this;
        }
        public Parser str (){return str (null);}
        public Parser str (Type type){
            elements.Add(new StrToken(type));
            return this;
        }
        /// <summary>
        /// 通过分词器返回一棵语法树
        /// </summary>
        /// <param name="lexer"></param>
        /// <returns></returns>
        public ASTree Parse(Lexer lexer)
        {
            ArrayList<ASTree> results=new ArrayList<ASTree>();
            foreach (var element in elements)
            {
                //通过分词器获取很多子树添加到results中
                element.Parse(lexer, results);
            }
            //构建子树组成的父树
            return factory.Make(results);
        }

        protected internal bool Match(Lexer lexer)
        {
            if(elements.Count==0)
                return true;
            else
            {
                Element e = elements[0];
                return e.Match(lexer);
            }
        }
        internal class IdToken : AToken
        {
            HashSet<string> reserved;

            public IdToken(Type type, HashSet<string> r) : base(type)
            {
                reserved = r != null ? r : new HashSet<string>();
            }

            protected override bool Test(Token t)
            {
                return t.IsIdentifier && !reserved.Contains(t.Text);
            }
        }

        internal class NumToken : AToken
        {
            private int value;

            public NumToken(Type t) : base(t)
            {
            }

            protected override bool Test(Token t)
            {
                return t.IsNumber;
            }
        }

        internal class StrToken : AToken
        {
            private string literal;

            public StrToken(Type t) : base(t)
            {
            }

            protected override bool Test(Token t)
            {
                return t.IsString;
            }
        }

    }
}