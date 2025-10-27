using System.Text.RegularExpressions;

namespace Horoscope.Infraestructure.Validators
{
    public class EmailValidator : IFieldValidator
    {
        private static readonly Regex _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        
        public string ErrorMessge(string fieldName)
            => $"El campo '{fieldName}' debe contener un correo valido";

        public bool IsValid(object? value)
        {
            if (value is not string email)
                return false;

            return _regex.IsMatch(email);
        }
    }
}
