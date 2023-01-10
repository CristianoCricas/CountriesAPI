using CountriesAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace CountriesAPI.Domain.Entities
{
    public class CountrySubdivisionEntity : EntityBase
    {
        #region FIELDS
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(5, MinimumLength = 1)]
        public string SubCode { get; set; } = string.Empty;

        [Required]
        public Guid CountryId { get; set; }
        #endregion


        #region ACTIONS
        /// <inheritdoc />
        public override void CopyFrom(EntityBase newEntity)
        {
            var newSubdivision = (CountrySubdivisionEntity)newEntity;
            base.CopyFrom(newSubdivision);

            Name = newSubdivision.Name;
            Category = newSubdivision.Category;
            SubCode = newSubdivision.SubCode;
        }
        #endregion
    }
}
