using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ManagerTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesService.AddCountry(request);
            });


        }

        [Fact]
        public void AddCountry_NullCountryName()
        {
            CountryAddRequest request = new CountryAddRequest { CountryName = null};

            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request);
            });
        }

        [Fact]
        public void AddCountry_DuplicateCountry() {

            CountryAddRequest request1 = new CountryAddRequest { CountryName = "Japan" };
            CountryAddRequest request2 = new CountryAddRequest { CountryName = "Japan" };

            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });

        }

        [Fact]
        public void AddCountry_ProperCountry()
        {
            CountryAddRequest request = new CountryAddRequest { CountryName = "Japan" };
            
            CountryResponse response = _countriesService.AddCountry(request);
                        
            List<CountryResponse> allCountries = _countriesService.GetAllCountries();

            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, allCountries);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            List<CountryResponse> countryResponses = _countriesService.GetAllCountries();

            Assert.Empty(countryResponses);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            List<CountryAddRequest> countryAddRequests = new List<CountryAddRequest>
            {
                new CountryAddRequest { CountryName = "USA" },
                new CountryAddRequest { CountryName = "UK" },
            };

            List<CountryResponse> responses= new List<CountryResponse>();
            List<CountryResponse> actualResponses = new List<CountryResponse>();
            foreach (CountryAddRequest request in countryAddRequests)
            {
                responses.Add(_countriesService.AddCountry(request));
            }
            actualResponses = _countriesService.GetAllCountries();

            foreach (CountryResponse country in responses)
            {
                Assert.Contains(country,actualResponses);
            }
        }

        #endregion

        #region GetCountryById

        [Fact]
        public void GetCountryById_NullId() {

            Guid? countryId = null;

            CountryResponse countryResponse = _countriesService.GetCountryById(countryId);

            Assert.Null(countryResponse);
        }

        [Fact]
        public void GetCountryById_ProperId() 
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest { CountryName = "Egypt" };
            CountryResponse countryResponse_Add = _countriesService.AddCountry(countryAddRequest);
            
            Guid guid = countryResponse_Add.CountryId;
            CountryResponse countryResponse_Get = _countriesService.GetCountryById(guid);

            Assert.Equal(countryResponse_Get, countryResponse_Add);

        
        }
        #endregion

    }
}