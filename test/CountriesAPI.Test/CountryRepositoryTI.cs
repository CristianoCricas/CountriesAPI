using CountriesAPI.Data;
using CountriesAPI.Data.Repositories;
using CountriesAPI.Domain.Entities;
using System.Diagnostics.Metrics;

namespace CountriesAPI.Test
{
    public class CountryRepositoryTI : IDisposable
    {
        readonly DbCountriesContext _context;
        readonly CountryRepository _countryRepo;

        /// <summary>
        /// xUnit TestInitialize
        /// </summary>
        public CountryRepositoryTI()
        {
            TestDbContextFactory factory = new TestDbContextFactory();
            _context = factory.CreateDbContext(new string[] { });

            //delete and recreate "countries_db_TEST" to make Clean tests
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _countryRepo = new CountryRepository(_context);
        }

        /// <summary>
        /// xUnit "TestCleanup"
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact(DisplayName = "Database connection should be OK.")]
        public void DbConnectionTest()
        {
            var canConnect = _context.Database.CanConnect();
            Assert.True(canConnect);
        }


        [Fact]
        public void Country_CRUD_Test()
        {
            var country = new CountryEntity
            {
                Name = "CRUD_Test",
                Alpha2Code = "CR",
                Alpha3Code = "CRU",
                NumericCode = 000,
                Independent = true
            };

            //before insert
            var findCountries = _countryRepo.ListAll(country.Name, country.Alpha2Code, country.Alpha3Code).Result;
            Assert.False(findCountries.Any(), $"Country {country.Name} shouldn't exist yet");

            //inserting
            bool inserted = _countryRepo.InsertOrUpdate(country).Result;
            Assert.True(inserted, "Country have to be inserted");

            //updating
            country.Name = "CRUD_Test_UPDATED";
            bool updated = _countryRepo.InsertOrUpdate(country).Result;
            Assert.True(updated, "Country have to be updated");

            //finding updated
            findCountries = _countryRepo.ListAll(country.Name, country.Alpha2Code, country.Alpha3Code).Result;
            Assert.True(findCountries.Any(), $"Country {country.Name} have to exists here");

            //deleting
            var deleted = _countryRepo.Delete(country.Id).Result;
            Assert.True(deleted, "Country have to be deleted");

            //try to find deleted
            findCountries = _countryRepo.ListAll(country.Name, country.Alpha2Code, country.Alpha3Code).Result;
            Assert.False(findCountries.Any(), $"Country {country.Name} cannot exist here");
        }


        [Fact]
        public void Subdivision_CRUD_Test()
        {
            var country = new CountryEntity
            {
                Name = "Sub_Test",
                Alpha2Code = "SS",
                Alpha3Code = "SSS",
                NumericCode = 444,
                Independent = true
            };

            //insert Country to be the "Father" of the subdivision
            _ = _countryRepo.InsertOrUpdate(country).Result;


            var subdivision = new CountrySubdivisionEntity
            {
                Name = "CRUDivision_Test",
                Category = "division",
                SubCode = "CS",
                CountryId = country.Id
            };

            //before insert
            var findSubdivisions = _countryRepo.ListAllSubdivisions(subdivision.CountryId).Result;
            Assert.False(findSubdivisions.Any(), $"Subdivision {subdivision.Name} shouldn't exist yet");

            //inserting
            string inserted = _countryRepo.InsertOrUpdateSubdivision(subdivision).Result;
            Assert.True(inserted == string.Empty, $"Subdivision have to be inserted, actual error {inserted}");

            //updating
            subdivision.Name = "CRUDivision_Test_UPDATED";
            string updated = _countryRepo.InsertOrUpdateSubdivision(subdivision).Result;
            Assert.True(updated == string.Empty, $"Subdivision have to be inserted, actual error {updated}");

            //finding updated
            var subdivisionPersisted = _countryRepo.SelectSubdivisionById(subdivision.CountryId, subdivision.Id).Result;
            Assert.True(subdivisionPersisted != null, $"Subdivision {subdivisionPersisted.Name} have to exists here");

            //deleting
            var deleted = _countryRepo.DeleteSubdivision(subdivision.CountryId, subdivision.Id).Result;
            Assert.True(deleted, "Subdivision have to be deleted");

            //try to find deleted
            subdivisionPersisted = _countryRepo.SelectSubdivisionById(subdivision.CountryId, subdivision.Id).Result;
            Assert.True(subdivisionPersisted == null, $"Subdivision cannot exists here");


            //deleting father (cleanup)
            _ = _countryRepo.Delete(country.Id).Result;
        }


        [Fact]
        public void Country_List_Search_Total_Test()
        {
            var country1 = new CountryEntity
            {
                Name = "SEARCH_Test_1",
                Alpha2Code = "CC",
                Alpha3Code = "CCC",
                NumericCode = 111,
                Independent = true
            };
            var country2 = new CountryEntity
            {
                Name = "Search_Test_2", 
                Alpha2Code = "RR",
                Alpha3Code = "RRR",
                NumericCode = 222,
                Independent = true
            };
            var country3 = new CountryEntity
            {
                Name = "SEEEARCH_Test_3",
                Alpha2Code = "UU",
                Alpha3Code = "UUU",
                NumericCode = 333,
                Independent = true
            };

            //inserting 
            _ = _countryRepo.InsertOrUpdate(country1).Result;
            _ = _countryRepo.InsertOrUpdate(country2).Result;
            _ = _countryRepo.InsertOrUpdate(country3).Result;

            //Finding by ID
            var country1ById = _countryRepo.SelectById(country1.Id).Result;
            var country2ById = _countryRepo.SelectById(country2.Id).Result;
            var country3ById = _countryRepo.SelectById(country3.Id).Result;
            Assert.True(country1ById != null, "Country 1 have to have exist (selected from BD by ID)");
            Assert.True(country2ById != null, "Country 2 have to have exist (selected from BD by ID)");
            Assert.True(country3ById != null, "Country 3 have to have exist (selected from BD by ID)");

            //List all 
            var countriesAll = _countryRepo.ListAll(null, null, null).Result;
            Assert.True(countriesAll.Count() == 3, $"Have to find ALL 3 Countries here");

            //Finding by NAME contains
            var countriesByName = _countryRepo.ListAll("SEARCH", null, null).Result;
            Assert.True(countriesByName.Count() == 2, $"Have to find 2 Countries here by NAME 'SEARCH'");

            //Finding by Alpha2
            var countriesByAlpha2 = _countryRepo.ListAll(null, country2.Alpha2Code, null).Result;
            Assert.True(countriesByAlpha2.Count() == 1, $"Have to find 1 Country here by Alpha2Code");

            //Finding by Alpha3
            var countriesByAlpha3 = _countryRepo.ListAll(null, null, country3.Alpha3Code).Result;
            Assert.True(countriesByAlpha3.Count() == 1, $"Have to find 1 Country here by Alpha3Code");

            //Search from multiple IDs
            Guid[] ids = { country1.Id, country2.Id };
            var countriesByIds = _countryRepo.SearchCountriesByIds(ids).Result;
            Assert.True(countriesByIds.Count() == 2, $"Have to find 2 Countries here by IDs");


            //deleting (cleanup)
            _ = _countryRepo.Delete(country1.Id).Result;
            _ = _countryRepo.Delete(country2.Id).Result;
            _ = _countryRepo.Delete(country3.Id).Result;
        }


        [Fact]
        public void Subdivisions_List_Search_Total_Test()
        {
            var country1 = new CountryEntity
            {
                Name = "Sub_Test",
                Alpha2Code = "SS",
                Alpha3Code = "SSS",
                NumericCode = 555,
                Independent = true
            };
            var country2 = new CountryEntity
            {
                Name = "Sub2_Test",
                Alpha2Code = "XX",
                Alpha3Code = "XXX",
                NumericCode = 999,
                Independent = true
            };

            //insert Country to be the "Father" of the subdivision
            _ = _countryRepo.InsertOrUpdate(country1).Result;
            _ = _countryRepo.InsertOrUpdate(country2).Result;


            var subdivision1A = new CountrySubdivisionEntity
            {
                Name = "CRUDivision_Test_1A",
                Category = "division",
                SubCode = "1A",
                CountryId = country1.Id
            };
            var subdivision1B = new CountrySubdivisionEntity
            {
                Name = "CRUDivision_Test_1B",
                Category = "state",
                SubCode = "1B",
                CountryId = country1.Id
            };
            var subdivision1C = new CountrySubdivisionEntity
            {
                Name = "CRUDivision_Test_1C",
                Category = "district",
                SubCode = "1C",
                CountryId = country1.Id
            };

            var subdivision2A = new CountrySubdivisionEntity
            {
                Name = "CRUDivision_Test_2A",
                Category = "district",
                SubCode = "2A",
                CountryId = country2.Id
            };

            //inserting 
            _ = _countryRepo.InsertOrUpdateSubdivision(subdivision1A).Result;
            _ = _countryRepo.InsertOrUpdateSubdivision(subdivision1B).Result;
            _ = _countryRepo.InsertOrUpdateSubdivision(subdivision1C).Result;
            _ = _countryRepo.InsertOrUpdateSubdivision(subdivision2A).Result;

            //Finding by ID
            var subdivision1AbyId = _countryRepo.SelectSubdivisionById(subdivision1A.CountryId, subdivision1A.Id).Result;
            var subdivision1BbyId = _countryRepo.SelectSubdivisionById(subdivision1B.CountryId, subdivision1B.Id).Result;
            var subdivision1CbyId = _countryRepo.SelectSubdivisionById(subdivision1C.CountryId, subdivision1C.Id).Result;
            var subdivision2AbyId = _countryRepo.SelectSubdivisionById(subdivision2A.CountryId, subdivision2A.Id).Result;
            Assert.True(subdivision1AbyId != null, "Subdivision 1A have to have exist (selected from BD by ID)");
            Assert.True(subdivision1BbyId != null, "Subdivision 1B have to have exist (selected from BD by ID)");
            Assert.True(subdivision1CbyId != null, "Subdivision 1C have to have exist (selected from BD by ID)");
            Assert.True(subdivision2AbyId != null, "Subdivision 2B have to have exist (selected from BD by ID)");

            //List all 
            var subdivisions1All = _countryRepo.ListAllSubdivisions(country1.Id).Result;
            Assert.True(subdivisions1All.Count() == 3, $"Have to find 3 Subdivisions here");

            var subdivisions2All = _countryRepo.ListAllSubdivisions(country2.Id).Result;
            Assert.True(subdivisions2All.Count() == 1, $"Have to find 1 Subdivisions here");


            //Search from multiple IDs
            Guid[] subIds = { subdivision1A.Id, subdivision1B.Id, subdivision1C.Id, subdivision2A.Id };
            var countriesByIds = _countryRepo.SearchSubdivisionsByIds(country1.Id, subIds).Result;
            Assert.True(countriesByIds.Count() == 3, $"Have to find only 3 Subdivisions of Country 1 here by IDs");


            //deleting (cleanup)
            _ = _countryRepo.DeleteSubdivision(subdivision1A.CountryId, subdivision1A.Id).Result;
            _ = _countryRepo.DeleteSubdivision(subdivision1B.CountryId, subdivision1B.Id).Result;
            _ = _countryRepo.DeleteSubdivision(subdivision1C.CountryId, subdivision1C.Id).Result;
            _ = _countryRepo.DeleteSubdivision(subdivision2A.CountryId, subdivision2A.Id).Result;

            //deleting fathers (cleanup)
            _ = _countryRepo.Delete(country1.Id).Result;
            _ = _countryRepo.Delete(country2.Id).Result;
        }


    }
}