using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class AnonymousFactory : Factory
    {
        public override Func<object, ASTree> Make0 { get; set; }
    }
    internal abstract class Factory
    {
        public const string factoryName = "Create";
        //protected abstract ASTree Make0(Object arg);
        public abstract Func<Object,ASTree> Make0 { get; set; }

        public ASTree Make(Object arg)
        {
            try
            {
                return Make0?.Invoke(arg);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e2)
            {
                throw e2;
            }
        }

        public static Factory Get(Type t,Type k)
        {
            Type clazz = t;
            Type argType = k;
            if (clazz == null)
                return null;
            try
            {
                MethodInfo m = clazz.GetMethod(factoryName,BindingFlags.Static|BindingFlags.Public);
                if (m == null)
                {
                    throw new MissingMethodException(factoryName);
                }
                //构建程序集
                return new AnonymousFactory()
                {
                    Make0 = (arg) =>
                    {
                        return (ASTree)m.Invoke(null, new object[] { arg });
                    }
                };
            }
            catch (MissingMethodException e)
            {
                Console.WriteLine("Missing method");
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine("Missing method");
            }
            try
            {
                var c=clazz.GetConstructor(new Type[] { argType });
                return new AnonymousFactory()
                {
                    Make0 = (arg) =>
                    {
                        return (ASTree)c.Invoke(new object[] { arg });
                    }
                };
            }
            catch (MissingMethodException e)
            {
                Console.WriteLine("Missing method");
                return null;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Missing method");
                return null;
            }
            //try
            //{
            //    var c = clazz.GetConstructor(new Type[]{argType});
            //    return new 
            //    {
            //        protected ASTree Make0(Object arg) {return c.Invoke(arg);}
            //    }
            //}
        }
        internal static Factory GetForASTList(Type t)
        {
            Factory f = Get(t, typeof(List<ASTree>));
            if (f == null) {
                f = new AnonymousFactory()
                {
                    Make0 = (arg) =>
                    {
                        List<ASTree> results = (List<ASTree>)arg;
                        if (results.Count == 1)
                            return results[0];
                        else
                            return new ASTList(results);
                    }
                };
            }
            return f;
        }
    }
}
