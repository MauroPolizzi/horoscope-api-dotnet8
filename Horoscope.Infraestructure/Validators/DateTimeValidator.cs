
namespace Horoscope.Infraestructure.Validators
{
    public class DateTimeValidator : IFieldValidator
    {
        public bool IsValid(object? value)
            => value is DateTime date && date != default;

        public string ErrorMessge(string fieldName)
            => $"El campo '{fieldName}' es requerido";
    }
}
