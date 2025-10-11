using ServiceContracts.DTO.Enums;
using Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as DTO for inserting an new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person name can not be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Email can not be blank")]
        [EmailAddress(ErrorMessage = "Email value should be valid")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }

        /// <summary>
        /// Converts current person to person add request
        /// </summary>
        /// <returns>AddPersonRequest</returns>
        public Person ToPerson() 
        {
            return new Person()
            {  
                Name = Name, 
                Email = Email, 
                DateOfBirth = DateOfBirth, 
                Gender = Gender.ToString(), 
                CountryId = CountryId 
            };
        }

    }
}
