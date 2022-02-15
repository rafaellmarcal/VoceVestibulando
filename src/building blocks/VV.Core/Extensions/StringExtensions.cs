﻿using System.Linq;

namespace VV.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ApenasNumeros(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
