using CountriesAPI.Controllers;
using CountriesAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CountriesAPI.Test
{
    public class CountryTU
    {
        [Fact]
        public void CopyFromTest()
        {
            CountryEntity country1 = new()
            {
                Name = "CopyFromTest",
                Alpha2Code = "TT",
                Alpha3Code = "TST",
                NumericCode = 123,
                IsoCode = "ISO ??",
                Independent = true
            };

            CountryEntity country2 = new();
            country2.CopyFrom(country1);

            Assert.Equivalent(country1, country2);
        }

        [Fact]
        public void RequiredStringFieldsTest()
        {
            int Required = 3; //Fill with total STRING FIELDS with [Required]

            CountryEntity country = new();
            var results = TestHelpers.EntityValidate(country);

            Assert.True((results.Count == Required), $"REQUIRED STRING FIELDS: {Required} | ACTUAL VALIDATION: {results.Count}");
        }

        [Fact]
        public void ValidateTest()
        {
            CountryEntity country = new()
            {
                Name = "ValidateTest",
                Alpha2Code = "TT",
                Alpha3Code = "TST"
            };

            var results = TestHelpers.EntityValidate(country);
            Assert.True((results.Count == 0), $"HERE THE ENTITY MUST HAVE '0' VALIDATION ERRORS | ACTUAL VALIDATION: {results.Count}");
        }




    }
}