using System;
using System.Runtime.CompilerServices;
using Entities;
namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO that is used as return type for most of CountryService methods
    /// </summary>
    public class CountryResponse
    {
        public string? CountryName { get; set; }
        public Guid CountryId { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
            {
                return false;
            }

            if(obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }

            CountryResponse other = (CountryResponse)obj;

            return other.CountryId == this.CountryId && this.CountryName == other.CountryName; 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtension
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse() { CountryId = country.CountryId, CountryName = country.CountryName };
        }
    }
}
