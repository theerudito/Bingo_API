using System.Text.RegularExpressions;

namespace Bingo.Helper
{
    public class Validations
    {
        public static bool ValidateQuantity(int _numCard)
        {
            if (_numCard == 0) return false;

            return true;
        }

        public static bool ValidateTitle(string _title)
        {
            if (string.IsNullOrEmpty(_title)) return false;
            return true;
        }

        public static bool ValidateEmail(string _emailTo)
        {
            if (string.IsNullOrEmpty(_emailTo)) return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(_emailTo, pattern)) return false;

            return true;
        }
    }
}