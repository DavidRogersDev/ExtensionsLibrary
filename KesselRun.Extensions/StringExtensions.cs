using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace KesselRun.Extensions
{
    public static class StringExtensions
    {
        public const string NotFound = "__NOT_FOUND__";
        public const string PathParameter = "path";

        /// <summary>
        /// Find the index of the nth char in a string. For example, an UNC may have a long path with 7 slashes. You may want to find the 3rd slash (from the left).
        /// </summary>
        /// <param name="path"></param>
        /// <param name="number"></param>
        /// <param name="delimiter"></param>
        /// <returns>The index of the nth char in a string.</returns>
        public static int GetIndexOfNthChar(this string path, int number, char delimiter)
        {
            if (path.IsNull())
                throw new ArgumentNullException(string.Format("The \"{0}\" parameter cannot be null.", PathParameter), PathParameter);
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException(string.Format("The \"{0}\" parameter cannot be empty of composed only of white space.", PathParameter), PathParameter);

            return GetIndexOfNthCharHelper(path, number, 0, delimiter);
        }

        /// <summary>
        /// Recursive helper method for GetIndexOfNthChar.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="number"></param>
        /// <param name="runningCount">This really needs to have a value of 0.</param>
        /// <param name="delimiter"></param>
        /// <returns>The index of the nth char in a string.</returns>
        private static int GetIndexOfNthCharHelper(string path, int number, int runningCount, char delimiter)
        {
            int currentIndexOfDelimiter = path.IndexOf(delimiter);

            if (currentIndexOfDelimiter == -1)
                throw new ArgumentOutOfRangeException(string.Format("You have requested the position of the Nth char where there are less than N instances of the char in the string."));

            if (number == 0)
                return currentIndexOfDelimiter + runningCount;

            runningCount += currentIndexOfDelimiter + 1;

            return GetIndexOfNthCharHelper(path.Substring(currentIndexOfDelimiter + 1), number - 1, runningCount, delimiter);
        }

        /// <summary>
        /// Find the index of the nth char in a string, starting from the right end of the string. For example, an UNC may have a long path 
        /// with 7 slashes. You may want to find the 3rd slash (from the right).
        /// </summary>
        /// <param name="path"></param>
        /// <param name="number"></param>
        /// <param name="delimiter"></param>
        /// <returns>The index of the nth char in a string, from the right.</returns>
        public static int GetIndexOfNthCharFromRight(this string path, int number, char delimiter)
        {
            if (path.IsNull())
                throw new ArgumentNullException(string.Format("The \"{0}\" parameter cannot be null.", PathParameter), PathParameter);
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException(string.Format("The \"{0}\" parameter cannot be empty of composed only of white space.", PathParameter), PathParameter);

            return GetIndexOfNthCharFromRightHelper(path, number, delimiter);
        }

        /// <summary>
        /// Recursive helper method for GetIndexOfNthCharFromRight.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="number"></param>
        /// <param name="delimiter"></param>
        /// <returns>The index of the nth char in a string, from the right.</returns>
        private static int GetIndexOfNthCharFromRightHelper(string path, int number, char delimiter)
        {
            int currentIndexOfDelimiter = path.LastIndexOf(delimiter);

            if (currentIndexOfDelimiter == -1)
                throw new ArgumentOutOfRangeException(string.Format("You have requested the position of the Nth char where there are less than N instances of the char in the string."));


            return number == 0 ? currentIndexOfDelimiter : GetIndexOfNthCharFromRightHelper(path.Substring(0, currentIndexOfDelimiter), number - 1, delimiter);
        }


        /// <summary>
        /// Specifically meant for use with Windows UNCs. Gets path BETWEEN 2 slashes in an UNC.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="firstSlash"></param>
        /// <param name="secondSlash"></param>
        /// <returns>Gets path BETWEEN 2 slashes in an UNC as a string.</returns>
        public static string GetPartPath(this string path, int firstSlash, int secondSlash)
        {
            if (firstSlash == secondSlash)
                return string.Empty;

            return path.SubstringBetweenNthAndMth(
                path.GetIndexOfNthChar(firstSlash, Path.DirectorySeparatorChar),
                path.GetIndexOfNthChar(secondSlash, Path.DirectorySeparatorChar)
                );
        }

        public static string GetPartPathFromRight(this string path, int firstSlash, int secondSlash)
        {
            if (firstSlash == secondSlash)
                return string.Empty;

            return path.SubstringBetweenNthAndMth(
                GetIndexOfNthCharFromRight(path, secondSlash, Path.DirectorySeparatorChar), 
                GetIndexOfNthCharFromRight(path, firstSlash, Path.DirectorySeparatorChar)
                );
        }

        /// <summary>
        /// Finds a substring in a string where that substring "starts with" a certain string. 
        /// This reduces the code you would otherwise have to write to perform this function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns>The substring within the source string.</returns>
        public static string SubstringFrom(this string source, string start, int? count = null)
        {
            if (string.IsNullOrEmpty(source))
                return NotFound;

            if (string.IsNullOrEmpty(start))
                throw new ArgumentException(string.Format("The \"{0}\" parameter cannot be null or an empty string.", "start"), "start");

            int indexOfString = source.IndexOf(start, StringComparison.Ordinal);

            //  If the string is not contained in the source string at all, return the NotFound constant. The caller can guard against it.
            if (indexOfString < 0)
                return NotFound;

            if (!count.HasValue)
            {
                return source.Substring(indexOfString);
            }

            if (count.Value > 0)
            {
                return source.Substring(indexOfString, count.Value);
            }

            throw new Exception(string.Format("If the \"{0}\" parameter is not null, it must be greated than 0.", "count"));
        }

        /// <summary>
        /// Finds the substring from the nth char to the mth char. Unlike SubstringBetweenNthAndMth, the string returned includes the chars at the Nth and Mth potsitions.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>The substring from the nth char to the mth char.</returns>
        public static string SubstringFromNthToMth(this string source, int first, int second)
        {
            if (string.IsNullOrEmpty(source))
                return NotFound;

            if (second < first)
                throw new ArgumentOutOfRangeException("second", "The second index cannot be less than the first");
            if (first < 0)
                throw new ArgumentOutOfRangeException("first", "The first index cannot be less than 0");


            int count = second - first + 1;

            return source.Substring(first, count);
        }

        /// <summary>
        /// This method gets strings which are between characters. For example, if the string was - E:\i\am\a very log\path
        /// and you called SubstringBetweenNthAndMth(soure, 7, 18), it would retun "a very log".
        /// 
        /// You cannot use the same index for two arguments. E.g. str.SubstringBetweenNthAndMth(5,5) throws an ArgumentOutOfRangeException.
        /// You may want to look at SubstringFromNthToMth if that use case is a possibility.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>The substring between the nth char and the mth char.</returns>
        public static string SubstringBetweenNthAndMth(this string source, int first, int second)
        {
            if (string.IsNullOrEmpty(source))
                return NotFound;
            if (second < first)
                throw new ArgumentOutOfRangeException("second", "The second index cannot be less than the first.");
            if (first < 0)
                throw new ArgumentOutOfRangeException("first", "The first index cannot be less than 0.");

            int count = second - first - 1;
            int firstCharPastChar = first + 1;  // the first para char will be 1 past the "first" index because we want the string BETWEEN the two indexes.

            return source.Substring(firstCharPastChar, count);
        }

        /// <summary>
        /// This simple string extension is based on the VB6 "Right" function which returns a certain number of characters, counting from the right.
        /// </summary>
        public static string SubstringFromRight(this string source, int numberOfChars)
        {
            if (string.IsNullOrEmpty(source))
                return NotFound;

            if (source.Length > numberOfChars)
            {
                return source.Substring(source.Length - numberOfChars, numberOfChars);
            }

            throw new ArgumentOutOfRangeException("numberOfChars",
                string.Format("The string \"{0}\" has a length which is less than {1} characters.", source, numberOfChars));
        }

        public static DateTime ToDateTime(this string source, string format)
        {
            return DateTime.ParseExact(source, format, DateTimeFormatInfo.CurrentInfo);
        }

        public static string TrimSuffixFromEnd(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(suffix))
                return source;

            var length = source.LastIndexOf(suffix, StringComparison.OrdinalIgnoreCase);

            return length > 0 ? source.Substring(0, length) : source;
        }

        public static MatchCollection GetPatternMatches(this string source, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.Matches(source);
        }

        public static List<int> GetIndexesOfPatternMatches(this string source, string pattern)
        {
            var matches = source.GetPatternMatches(pattern);

            return matches.Cast<Match>().Select(m => m.Index).ToList();
        }

        public static int GetNumberOfPatternMatches(this string source, string pattern)
        {
            return source.GetPatternMatches(pattern).Count;
        }

        /// <summary>
        /// Replace invalid characters in a string with empty strings. 
        /// </summary>
        /// <param name="stringToClean">Type: System.String. The string to parse for illegal characters.</param>
        /// <returns>Type: System.String. A string stripped of the illegal characters.</returns>
        public static string RemoveIllegalCharacters(this string stringToClean)
        {
            try
            {
                return Regex.Replace(
                    stringToClean, @"[<>:\""/\\|?*]", // These are illegal for file/directory naming purposes in Windows.
                    string.Empty,
                    RegexOptions.None,
                    TimeSpan.FromSeconds(1.5))
                    .Trim();
            }
            catch (RegexMatchTimeoutException)
            {
                // If we timeout when replacing invalid characters,  
                // we should return Empty. 
                return string.Empty;
            }
        }

        public static bool? TryParseAsBool(this string source)
        {
            bool boolean;

            if (bool.TryParse(source, out boolean))
            {
                return boolean;
            }

            return null;
        }
        
        public static int? TryParseAsInt(this string source)
        {
            int parsedInt;

            if (int.TryParse(source, out parsedInt))
            {
                return parsedInt;
            }

            return null;
        }

        public static int? TryParseAsInt(this string source, NumberStyles numberStyles, IFormatProvider provider)
        {
            int parsedInt;

            if (int.TryParse(source, numberStyles, provider, out parsedInt))
            {
                return parsedInt;
            }

            return null;
        }

        public static string FormatWith(this string source, params object[] args)
        {
            return string.IsNullOrWhiteSpace(source) ? source : string.Format(source, args);
        }

        public static bool IsEmpty(this string source)
        {
            if (source.IsNull()) throw new ArgumentNullException("source");
            return source.Equals(string.Empty);
        }
    }
}
