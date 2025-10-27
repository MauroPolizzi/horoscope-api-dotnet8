using Horoscope.Application.Dtos;
using System.Reflection;

namespace Horoscope.Infraestructure.Validators
{
    public class ValidatorService
    {
        public List<string> Validate<T>(T dto)
        {
            List<string> error = new List<string>();
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(dto);
                var attributes = prop.GetCustomAttributes();
                
                var validator = FieldValidatorFactory.GetValidator(prop.PropertyType, attributes);

                if (!validator.IsValid(value)) error.Add(validator.ErrorMessge(prop.Name));
            }

            return error;
        }
    }
}
