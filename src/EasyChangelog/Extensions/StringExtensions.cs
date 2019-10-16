using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChangelog.Extensions
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string source, string value)
        {
            if (!source.EndsWith(value))
                return source;

            return source.Remove(source.LastIndexOf(value));
        }
    }
}
