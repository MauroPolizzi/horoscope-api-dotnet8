
namespace Horoscope.Infraestructure.Validators
{
    public class StringValidator : IFieldValidator
    {
        public bool IsValid(object? value)
            => value is string s && !string.IsNullOrEmpty(s);

        public string ErrorMessge(string fieldName)
            => $"El campo '{fieldName}' es requerido";
    }
}
