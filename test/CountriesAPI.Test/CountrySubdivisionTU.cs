using CountriesAPI.Domain.Entities;

namespace CountriesAPI.Test
{
    public class CountrySubdivisionTU
    {
        [Fact]
        public void CopyFromTest()
        {
            CountrySubdivisionEntity subdivision1 = new()
            {
                Name = "CopyFromTest",
                Category = "categ",
                SubCode = "TU"
            };

            CountrySubdivisionEntity subdivision2 = new();
            subdivision2.CopyFrom(subdivision1);

            Assert.Equivalent(subdivision1, subdivision2);
        }

        [Fact]
        public void RequiredStringFieldsTest()
        {
            int Required = 3; //Fill with total STRING FIELDS with [Required]

            CountrySubdivisionEntity subdivision = new();
            var results = TestHelpers.EntityValidate(subdivision);

            Assert.True((results.Count == Required), $"REQUIRED STRING FIELDS: {Required} | ACTUAL VALIDATION: {results.Count}");
        }

        [Fact]
        public void ValidateTest()
        {
            CountrySubdivisionEntity subdivision = new()
            {
                Name = "ValidateTest",
                Category = "categ",
                SubCode = "TU"
            };

            var results = TestHelpers.EntityValidate(subdivision);
            Assert.True((results.Count == 0), $"HERE THE ENTITY MUST HAVE '0' VALIDATION ERRORS | ACTUAL VALIDATION: {results.Count}");
        }




    }
}