using CountriesAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CountriesAPI.Test
{
    public class TestHelpers
    {
        public static List<ValidationResult> EntityValidate<T>(T entity) where T : EntityBase
        {
            if (entity == null)
                throw new Exception("entity cannot be NULL to validade");

            entity.ValidateDefaultValues();

            var context = new ValidationContext(entity, null, null);
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(entity, context, result, true);

            return result;
        }
    }
}
