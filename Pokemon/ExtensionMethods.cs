using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public static class ExtensionMethods
    {
        public static string[] SplitBoosts(this string boosts)
        {
            return boosts.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitAttributes(this string boost)
        {
            return boost.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
