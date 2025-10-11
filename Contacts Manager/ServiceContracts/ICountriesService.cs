using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Contains business logic to manipulate Country Entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns the country object after adding it(including newly generated id) </returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Get all the countries
        /// </summary>
        /// <returns>Returns a list of all countries as CountryResponse</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Gets a country with ID
        /// </summary>
        /// <param name="id">The id of the country</param>
        /// <returns>The required country as CountryResponse</returns>
        CountryResponse? GetCountryById(Guid? id);
    }
}
