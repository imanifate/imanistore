using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Aplication.Utilities
{
    public class CodeGenerators
    {
        public static string ActiveCode() => new Random().Next(10000, 1000000).ToString();
    }
}
