using Pokemon.Models;
using System;

namespace Pokemon
{
    public static class ExtensionMethods
    {
        public static string[] SplitBoosts(this string boosts)
        {
            return boosts.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}