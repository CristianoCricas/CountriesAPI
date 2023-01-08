using CountriesAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace CountriesAPI.Domain.Entities
{
    public class CountryEntity : EntityBase
    {
        #region FIELDS
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression("^[A-Z]{2}")]
        public string Alpha2Code { get; set; } = string.Empty;

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression("^[A-Z]{3}")]
        public string Alpha3Code { get; set; } = string.Empty;

        [Required]
        public int NumericCode { get; set; }

        public string IsoCode { get; set; } = string.Empty;

        [Required]
        public bool Independent { get; set; }
        #endregion


        #region ACTIONS
        public override DataBaseAction ValidateDefaultValues()
        {
            if (!string.IsNullOrEmpty(Alpha2Code))
                IsoCode = $"ISO 3166-2:{Alpha2Code}";

            return base.ValidateDefaultValues();
        }

        public override void CopyFrom(EntityBase newEntity)
        {
            var newCountry = (CountryEntity)newEntity;
            base.CopyFrom(newCountry);

            Name = newCountry.Name;
            Alpha2Code = newCountry.Alpha2Code;
            Alpha3Code = newCountry.Alpha3Code;
            NumericCode = newCountry.NumericCode;
            IsoCode = newCountry.IsoCode;
            Independent = newCountry.Independent;
        }
        #endregion
    }
}
