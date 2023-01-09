using System.ComponentModel.DataAnnotations;

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

        [Required]
        public bool Independent { get; set; }


        public string IsoCode
        {
            get
            {
                if (!string.IsNullOrEmpty(Alpha2Code))
                    return $"ISO 3166-2:{Alpha2Code}";

                return string.Empty;
            }
        }
        #endregion


        #region ACTIONS
        /// <inheritdoc />
        public override void CopyFrom(EntityBase newEntity)
        {
            var newCountry = (CountryEntity)newEntity;
            base.CopyFrom(newCountry);

            Name = newCountry.Name;
            Alpha2Code = newCountry.Alpha2Code;
            Alpha3Code = newCountry.Alpha3Code;
            NumericCode = newCountry.NumericCode;
            Independent = newCountry.Independent;
        }
        #endregion
    }
}
