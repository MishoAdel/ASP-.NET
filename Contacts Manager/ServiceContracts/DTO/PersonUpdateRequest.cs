using Entities;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "Person Id can not be blank")]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "Person name can not be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email can not be blank")]
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
                PersonId = PersonId,
                Name = Name,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId
            };
        }

    }
}
