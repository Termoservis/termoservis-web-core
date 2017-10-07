using System;
using System.Linq;

namespace Termoservis.Libs.Extensions
{
	/// <summary>
	/// The <see cref="string"/> extensions.
	/// </summary>
	public static class StringExtensions
	{
        /// <summary>
        /// Makes string searchable.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <returns>Returns new instance of specified string that is searchable.</returns>
        /// <remarks>
        /// Converts to lower case, removes single char keywords and removes special characters (including language specific letters).
        /// </remarks>
        public static string AsSearchable(this string @string)
        {
            return string.Join(" ", @string.ToLowerInvariant()
                .Replace("š", "s")
                .Replace("ć", "c")
                .Replace("č", "c")
                .Replace("đ", "d")
                .Replace("ž", "z")
                .Replace("(", " ")
                .Replace(")", " ")
                .Replace("[", " ")
                .Replace("]", " ")
                .Replace("{", " ")
                .Replace("}", " ")
                .Replace("&", " ")
                .Replace("%", " ")
                .Replace("$", " ")
                .Replace("/", " ")
                .Replace("`", " ")
                .Replace("@", " ")
                .Replace("\"", " ")
                .Replace("!", " ")
                .Replace("'", " ")
                .Replace("*", " ")
                .Replace("_", " ")
                .Replace("-", " ")
                .Replace(".", " ")
                .Replace(",", " ")
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .Where(k => k.Length > 1)
                .Select(s => s.Trim()));
        }
	}
}
