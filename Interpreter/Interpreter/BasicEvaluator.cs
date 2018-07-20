using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class BasicEvaluator
    {
        public static int TRUE = 1;
        public static int FALSE = 0;
    }
    internal abstract partial class ASTree
    {
        public abstract Object eval(IEnvironment env);
    }

    internal partial class ASTList
    {
        public override Object eval(IEnvironment env)
        {
            throw new StoneException("cannot eval: " + ToString(), null);
        }
    }
    internal partial class ASTLeaf
    {
        public override Object eval(IEnvironment env)
        {
            throw new StoneException("cannot eval: " + ToString(), null);
        }
    }
    internal partial class NumberLiteral
    {
        public override Object eval(IEnvironment e) { return Value; }
    }
    internal partial class StringLiteral
    {
        public override Object eval(IEnvironment e) { return Value; }
    }
    internal partial class Name
    {
        public override Object eval(IEnvironment env)
        {
            Object value = env.get(NameValue);
            if (value == null)
                throw new StoneException("undefined name: " + NameValue, null);
            else
                return value;
        }
    }
    internal partial class NegativeExpr
    {
        public override Object eval(IEnvironment env)
        {
            Object v = ((ASTree)Operand()).eval(env);
            if (v is Int32)
                return (-((int)v));
            else
                throw new StoneException("bad type for -", null);
        }
    }

    internal partial class BinaryExpr
    {
        public override Object eval(IEnvironment env)
        {
            String op = Operator();
            if ("=".Equals(op))
            {
                Object right = ((ASTree)Right()).eval(env);
                return computeAssign(env, right);
            }
            else
            {
                Object left = ((ASTree)Left()).eval(env);
                Object right = ((ASTree)Right()).eval(env);
                return computeOp(left, op, right);
            }
        }
        protected Object computeAssign(IEnvironment env, Object rvalue)
        {
            ASTree l = Left();
            if (l is Name) {
                env.put(((Name)l).NameValue, rvalue);
                return rvalue;
            }
            else
                throw new StoneException("bad assignment", null);
        }
        protected Object computeOp(Object left, String op, Object right)
        {
            if (left is Int32 && right is Int32) {
                return computeNumber((Int32)left, op, (Int32)right);
            }
            else
                if (op.Equals("+"))
                return left.ToString() + right.ToString();
            else if (op.Equals("=="))
            {
                if (left == null)
                    return right == null ? BasicEvaluator.TRUE : BasicEvaluator.FALSE;
                else
                    return left.Equals(right) ? BasicEvaluator.TRUE : BasicEvaluator.FALSE;
            }
            else
                throw new StoneException("bad type", null);
        }
        protected Object computeNumber(Int32 left, String op, Int32 right)
        {
            int a = left;
            int b = right;
            if (op.Equals("+"))
                return a + b;
            else if (op.Equals("-"))
                return a - b;
            else if (op.Equals("*"))
                return a * b;
            else if (op.Equals("/"))
                return a / b;
            else if (op.Equals("%"))
                return a % b;
            else if (op.Equals("=="))
                return a == b ? BasicEvaluator.TRUE : BasicEvaluator.FALSE;
            else if (op.Equals(">"))
                return a > b ? BasicEvaluator.TRUE : BasicEvaluator.FALSE;
            else if (op.Equals("<"))
                return a < b ? BasicEvaluator.TRUE : BasicEvaluator.FALSE;
            else
                throw new StoneException("bad operator", null);
        }
    }
    internal partial class BlockStmnt
    {
        public override Object eval(IEnvironment env)
        {
            Object result = 0;
            foreach (ASTree t in this)
            {
                if (!(t is NullStmnt))
                    result = ((ASTree)t).eval(env);
            }
            return result;
        }
    }
    internal partial class IfStmnt
    {
        public override Object eval(IEnvironment env)
        {
            Object c = ((ASTree)condition()).eval(env);
            if (c is Int32 && (int)c != BasicEvaluator.FALSE)
                return ((ASTree)thenBlock()).eval(env);
            else {
                ASTree b = elseBlock();
                if (b == null)
                    return 0;
                else
                    return ((ASTree)b).eval(env);
            }
        }
    }
    internal partial class WhileStmnt
    {
        public override Object eval(IEnvironment env)
        {
            Object result = 0;
            for (; ; )
            {
                Object c = ((ASTree)condition()).eval(env);
                if (c is Int32 && ((int)c) == BasicEvaluator.FALSE)
                    return result;
                else
                    result = ((ASTree)body()).eval(env);
        }
    }
    }
}
