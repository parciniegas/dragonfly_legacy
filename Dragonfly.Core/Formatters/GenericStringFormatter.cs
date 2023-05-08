using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Dragonfly.Core.Formatters
{
    public class GenericStringFormatter : IFormatProvider, ICustomFormatter, IStringFormatter
    {
        #region Constructors

        public GenericStringFormatter()
        {
            DigitChar = '#';
            AlphaChar = '@';
            EscapeChar = '\\';
        }
        #endregion

        #region Properties
        public char AlphaChar { get; protected set; }
        public char DigitChar { get; protected set; }
        public char EscapeChar { get; protected set; }
        #endregion

        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public string Format(string input, string formatPattern)
        {
            if (!IsValid(formatPattern))
                throw new ArgumentException("FormatPattern is not valid");

            var returnValue = new StringBuilder();
            for (var x = 0; x < formatPattern.Length; ++x)
            {
                if (formatPattern[x] == EscapeChar)
                {
                    ++x;
                    returnValue.Append(formatPattern[x]);
                }
                else
                {
                    char nextValue;
                    input = GetMatchingInput(input, formatPattern[x], out nextValue);
                    if (nextValue != char.MinValue)
                        returnValue.Append(nextValue);
                }
            }
            return returnValue.ToString();
        }

        protected virtual bool IsValid(string formatPattern)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(formatPattern), "FormatPattern");
            var escapeCharFound = false;
            foreach (var t in formatPattern)
            {
                if (escapeCharFound && t != DigitChar
                    && t != AlphaChar
                    && t != EscapeChar)
                    return false;

                if (escapeCharFound)
                    escapeCharFound = false;
                else
                    escapeCharFound |= t == EscapeChar;
            }

            return !escapeCharFound;
        }

        protected virtual string GetMatchingInput(string input, char formatChar, out char matchChar)
        {
            var digit = formatChar == DigitChar;
            var alpha = formatChar == AlphaChar;
            if (!digit && !alpha)
            {
                matchChar = formatChar;
                return input;
            }
            var index = 0;
            matchChar = char.MinValue;
            for (var x = 0; x < input.Length; ++x)
            {
                if ((!digit || !char.IsDigit(input[x])) && (!alpha || !char.IsLetter(input[x]))) 
                    continue;

                matchChar = input[x];
                index = x + 1;
                break;
            }

            return input.Substring(index);
        }
    }
}
