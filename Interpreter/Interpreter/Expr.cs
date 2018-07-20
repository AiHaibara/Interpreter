using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class Expr:Element
    {
        internal Factory factory;
        protected Operators ops;
        protected Parser factor;
        internal Expr(Type t, Parser exp,
                       Operators map)
        {
            factory = Factory.GetForASTList(t);
            ops = map;
            factor = exp;
        }
        protected internal override void Parse(Lexer lexer, List<ASTree> res) 
        {
            ASTree right = factor.Parse(lexer);
            Precedence prec;
            while ((prec = nextOperator(lexer)) != null)
                right = doShift(lexer, right, prec.value);

            res.Add(right);
        }
        private ASTree doShift(Lexer lexer, ASTree left, int prec)
        {
            ArrayList<ASTree> list = new ArrayList<ASTree>();
            list.Add(left);
            list.Add(new ASTLeaf(lexer.Read()));
            ASTree right = factor.Parse(lexer);
            Precedence next;
            while ((next = nextOperator(lexer)) != null
                    && rightIsExpr(prec, next))
            right = doShift(lexer, right, next.value);

            list.Add(right);
            return factory.Make(list);
        }
        private Precedence nextOperator(Lexer lexer) 
        {
             Token t = lexer.Peek(0);
            if (t.IsIdentifier) {
                ops.TryGetValue(t.Text, out var ret);
                return ret;
            }
            else
                return null;
        }
        private static bool rightIsExpr(int prec, Precedence nextPrec)
        {
            if (nextPrec.leftAssoc)
                return prec < nextPrec.value;
            else
                return prec <= nextPrec.value;
        }
        protected internal override bool Match(Lexer lexer) 
        {
            return factor.Match(lexer);
        }
    }
}
