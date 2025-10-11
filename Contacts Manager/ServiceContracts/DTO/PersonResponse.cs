using Entities;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents as DTO return type for add new person
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country {  get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        public double? Age { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse other = (PersonResponse)obj;
            return this.PersonId == other.PersonId 
                && this.Name == other.Name
                && this.Email == other.Email
                && this.DateOfBirth == other.DateOfBirth
                && this.Gender == other.Gender
                && this.CountryId == other.CountryId
                && this.Country == other.Country
                && this.Address == other.Address
                && this.ReceiveNewsLetter == other.ReceiveNewsLetter
                && this.Age == other.Age;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"PersonID :{PersonId}, " +
                $"Name: {Name}, " +
                $"Email: {Email} " +
                $"Date of Birth: {DateOfBirth?.ToString()} " +
                $"Gender: {Gender?.ToString()} " +
                $"CountryID: {CountryId} " +
                $"Country: {Country} " +
                $"Address: {Address} " +
                $"Age: {Age} ";
        }


        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonId = this.PersonId,
                Name = this.Name,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                ReceiveNewsLetter = this.ReceiveNewsLetter,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), this.Gender,true),
                CountryId = this.CountryId,
                Address = this.Address,
                
            };
        }
    }

    public static class PersonExtensions
    {
        /// <summary>
        /// Extension method to convert Person to person Response
        /// </summary>
        /// <param name="person">Person object you want to convert</param>
        /// <returns>PersonResponse object</returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonId = person.PersonId,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Address = person.Address,
                ReceiveNewsLetter = person.ReceiveNewsLetter,
                CountryId = person.CountryId,
                Gender = person.Gender,
                Age = (person.DateOfBirth != null)? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays /365.25) : null,
            };
        }


    }
}
