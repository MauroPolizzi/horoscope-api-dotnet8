using Horoscope.Application.Dtos;

namespace Horoscope.Infraestructure.Validators
{
    public static class FieldValidatorFactory
    {
        public static IFieldValidator GetValidator(Type type, IEnumerable<Attribute> attributes)
        {
            if (attributes.Any(a => a is EmailFieldAttribute))
                return new EmailValidator();

            if (type == typeof(string))
                return new StringValidator();
            
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return new DateTimeValidator();

            if (type == typeof(int) || type == typeof(int?))
                return new IntValidator();

            throw new NotSupportedException($"No hay validador definido para el tipo {type.Name}");
        }
    }
}
