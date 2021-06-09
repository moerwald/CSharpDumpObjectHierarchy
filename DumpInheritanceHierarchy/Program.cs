using System;
using System.Collections.Generic;
using System.Linq;

namespace DumpInheritanceHierarchy
{
    class A { }

    class B:A { }

    class C:B { }

    class D:C { }
    
    public static class HierarchyDumper
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type) => 
            type.BaseType == null ? new Type[0] 
                                  : Enumerable.Repeat(type.BaseType, 1)
                                              .Concat(type.BaseType.GetBaseTypes() /* recursive call */);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var d = new D();
            var result = d.GetType().GetBaseTypes().ToList();
            // Prints: "C->B->A->Object"
            Console.WriteLine($"{string.Join("->", result.Select(t => t.Name))}");
        }
    }
}
