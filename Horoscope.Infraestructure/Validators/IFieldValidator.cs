namespace Horoscope.Infraestructure.Validators
{
    public interface IFieldValidator
    {
        bool IsValid(object? value);
        string ErrorMessge(string fieldName);
    }
}
