﻿using System.Text.RegularExpressions;

namespace CommonHelpers
{
    /// <summary>
    /// Class that encapsulates methods simplifying the work with Regular Expressions
    /// </summary>
    public class RegexUtilities
    {
        /// <summary>
        /// Method that checks if the regular expression matches the string
        /// </summary>
        /// <param name="regexPattern">
        /// The regular expression
        /// </param>
        /// <param name="testedString">
        /// The tested string
        /// </param>
        /// <returns>
        /// True if the regex matches the string, false otherwise
        /// </returns>
        public static bool RegexIsMatchString(string regexPattern, string testedString)
        {
            Regex regex = new Regex(regexPattern);
            return regex.IsMatch(testedString);
        }
    }
}
