using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionMaths
{
    public static class CommonMaths
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
    }
}
