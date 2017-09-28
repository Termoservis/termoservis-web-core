namespace Termoservis.Common.Extensions
{
	/// <summary>
	/// The <see cref="string"/> extensions.
	/// </summary>
	public static class StringExtensions
	{
        /// <summary>
        /// Transforms specified string to FTS string.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns>Returns the transformed FTS string.</returns>
        public static string AsFtsContainsString(this string keywords)
        {
            return $"({FullTextSearchModelUtil.FullTextContains}{ConvertWithAll(keywords)})";
        }

        /// <summary>
        /// Converts search keywords to keywords string seperated by AND keyword.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>Returns keywords string seperated by AND keyword.</returns>
        private static string ConvertWithAll(string search)
        {
            if (string.IsNullOrWhiteSpace(search) || search.StartsWith("\"") && search.EndsWith("\""))
                return search;
            return "\"" + string.Join("*\" and \"", search.Split(' ', '　').Where(c => c != "and")) + "*\"";
        }

        /// <summary>
        /// Makes string searchable.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <returns>Returns new instance of specified string that is searchable.</returns>
        public static string AsSearchable(this string @string)
		{
			return @string.ToLowerInvariant()
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
                .Replace("  ", " ")
                .Replace("  ", " ")
                .Replace("  ", " ")
                .Trim();
		}
	}
}
