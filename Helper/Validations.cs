using System.Text.RegularExpressions;

namespace Bingo.Helper
{
    public class Validations
    {
        private bool ValidateField(string _title, int _numCard)
        {
            if (string.IsNullOrEmpty(_title))
            {
                // _isSending = "El título es requerido.";

                return false;
            }
            if (_numCard == 0)
            {
                //  _isSending = "Mayor a cero es requerido.";

                return false;
            }
            return true;
        }

        private bool ValidateEmail(string _emailTo)
        {
            if (string.IsNullOrEmpty(_emailTo))
            {
                // _isSending = "El email es requerido.";

                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(_emailTo, pattern))
            {
                // _isSending = "No es un email válido.";

                return false;
            }

            return true;
        }
    }
}