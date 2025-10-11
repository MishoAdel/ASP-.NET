using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using Services.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonsDbContext _db;
        private readonly ICountriesService _countriesService;

        public PersonService(PersonsDbContext personsDbContext,ICountriesService countriesService)
        {
            _db = personsDbContext;
            _countriesService = countriesService;
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryById(person.CountryId)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest request)
        {
            if(request == null) 
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            Person person = request.ToPerson();

            person.PersonId = Guid.NewGuid();

            _db.Persons.Add(person);
            _db.SaveChanges();

            PersonResponse personResponse = person.ToPersonResponse();

            personResponse.Country = _countriesService.GetCountryById(person.CountryId)?.CountryName;
            return (personResponse);

        }



        public List<PersonResponse> GetAllPeople()
        {
            return _db.Persons.ToList().Select(temp =>   ConvertPersonToPersonResponse(temp)).ToList();
        }

        public PersonResponse? GetPersonByID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = _db.Persons.FirstOrDefault(temp => temp.PersonId == personID); 
            
            if (person == null)
                return null;
            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPerson(string searchBy, string searchString)
        {
            List<PersonResponse> people = GetAllPeople();
            List<PersonResponse> filteredPeople = new List<PersonResponse>();

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return people;

            switch (searchBy) {
                case (nameof(PersonResponse.Name)):
                    filteredPeople = people.Where(
                        temp => (!string.IsNullOrEmpty(temp.Name) ?
                        temp.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true)).ToList();
                    break;

                case (nameof(PersonResponse.Email)):
                    filteredPeople = people.Where(
                        temp => (!string.IsNullOrEmpty(temp.Email) ?
                        temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                    break;

                case (nameof(PersonResponse.DateOfBirth)):
                    filteredPeople = people.Where(
                        temp => (temp.DateOfBirth != null ?
                        temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                    break;

                case (nameof(PersonResponse.Gender)):
                    filteredPeople = people.Where(
                        temp => (!string.IsNullOrEmpty(temp.Gender) ?
                        temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                    break;

                case (nameof(PersonResponse.CountryId)):
                    filteredPeople = people.Where(
                        temp => (!string.IsNullOrEmpty(temp.CountryId.ToString()) ?
                        temp.CountryId.ToString()!.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                    break;


                case (nameof(PersonResponse.Address)):
                    filteredPeople = people.Where(
                        temp => (!string.IsNullOrEmpty(temp.Address) ?
                        temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                    break;

                default:
                    filteredPeople = people;
                    break;

            }
            return filteredPeople;
        }

        public List<PersonResponse> GetSortedPeople(List<PersonResponse> allPeople, string sortBy, SortOrderOptions sortOrder)
        {
            if(string.IsNullOrEmpty(sortBy))
                return allPeople;

            List<PersonResponse> sortedPeople = (sortBy,sortOrder) switch
            {
                (nameof(PersonResponse.Name),SortOrderOptions.Asc) 
                => allPeople.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Name), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOptions.Asc)
                => allPeople.OrderBy(temp => temp.ReceiveNewsLetter).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOptions.Desc)
                => allPeople.OrderByDescending(temp => temp.ReceiveNewsLetter).ToList(),

                _ => allPeople
            };

            return sortedPeople;
        }

        public PersonResponse? UpdatePerson(PersonUpdateRequest? request)
        {
            if(request == null) 
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            Person? myPerson = _db.Persons.FirstOrDefault(temp => temp.PersonId == request.PersonId);

            if (myPerson == null)
                throw new ArgumentException("Person is not found");

            myPerson.Name = request.Name;
            myPerson.Email = request.Email;
            myPerson.Address = request.Address;
            myPerson.Gender = request.Gender.ToString();
            myPerson.ReceiveNewsLetter = request.ReceiveNewsLetter;
            myPerson.DateOfBirth = request.DateOfBirth;
            myPerson.CountryId = request.CountryId;

            _db.SaveChanges();

            return myPerson.ToPersonResponse();

        }

        public bool DeletePerson(Guid personId)
        {
            if(personId == Guid.Empty)
                return false;

            Person? person = _db.Persons.FirstOrDefault(temp => temp.PersonId == personId);

            if (person == null)
                return false;

            _db.Persons.Remove(person);
            _db.SaveChanges();
            return true;
        }
    }
}
