using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs7Features
{
    /// <summary>
    /// Testing of ValueTuple structure introduced in C# 7.0
    /// </summary>
    public static class TuplesTest
    {
        //Tuples, interfaces and inheritance
        private interface ITpl
        {
            (int a, string s) Tpl { get; set; }

            //Compiler generates the following. Tuple's elements names go to the TupleElementNames attribute
            //[TupleElementNames(new string[] { "a", "s" })]
            //ValueTuple<int, string> Tpl { [return: TupleElementNames(new string[] { "a", "s" })] get; [param: TupleElementNames(new string[] { "a", "s" })] set; }
        }

        private class TplBase: ITpl
        {
            //Below lines will not work
            //public (int, string) Tpl { get; set; }
            //public (int k, string p) Tpl { get; set; }
            public virtual (int a, string s) Tpl { get; set; }
        }

        private class TplDerived: TplBase
        {
            //Below lines will not work
            //public override (int, string) Tpl { get; set; }
            //public override (int k, string p) Tpl { get; set; }
            public override (int a, string s) Tpl { get; set; }
        }

        public static void Run()
        {
            var tpl = (1, "a");
            Console.WriteLine($"Item1: {tpl.Item1}, Item2: {tpl.Item2}");


            //Give a name to every element in tuple
            var tplName = (d: 2, s: "b");
            Console.WriteLine($"Item1: {tplName.d}, Item2: {tplName.s}");


            //We can still adress tuple's items by Item1, Item2 etc.
            Console.WriteLine($"Item1: {tplName.Item1}, Item2: {tplName.Item2}");


            //Give a name to the last item only
            var tplHalfName = (2, s: "b");
            Console.WriteLine($"Item1: {tplHalfName.Item1}, Item2: {tplHalfName.s}");


            //Tuples with the same elements and different names are compatible
            var lst = new List<(int, string)> {(3, "c"), tpl, tplName, tplHalfName};
            foreach (var tuple in lst)
            {
                Console.WriteLine($"Item1: {tuple.Item1}, Item2: {tuple.Item2}");
            }

            //How equals work Equals
            var eq = tplName.Equals(tplHalfName);
            Console.WriteLine($"tplName.Equals(tplHalfName) is {eq}");

            //Below line will not work
            //var eq2 = tplName == tplHalfName;

            //Tuple deconstruction
            var (x, y) = tpl;
            Console.WriteLine($"x: {x}, y: {y}");

            //Tuple deconstruction with discard pattern
            var (_, y2) = tplName;
            Console.WriteLine($"y2: {y2}");

            //Using tuples with functions
            var tplFromFunc2 = FuncReturnTuple2();
            Console.WriteLine($"Item1: {tplFromFunc2.i1}, Item2: {tplFromFunc2.i2}");

            Console.ReadKey();
        }

        private static (int, string) FuncReturnTuple()
        {
            //Compile will warn about incorreect usage of items names
            //return (i1: 4, i2: "d");
            return (4, "d");
        }

        private static (int i1, string i2) FuncReturnTuple2()
        {
            return (i1: 5, i2: "e");
        }
    }
}
