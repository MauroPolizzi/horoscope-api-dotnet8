namespace Horoscope.Infraestructure.Validators
{
    public class IntValidator : IFieldValidator
    {
        public bool IsValid(object? value)
            => value is int i && i != 0;

        public string ErrorMessge(string fieldName)
            => $"El campo '{fieldName}' es requerido";
    }
}
