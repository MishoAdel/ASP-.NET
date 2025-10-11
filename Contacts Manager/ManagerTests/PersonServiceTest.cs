using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using ServiceContracts.DTO.Enums;
using Xunit.Abstractions;
using Entities;

namespace ManagerTests
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;


        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService(false);
            _countriesService = new CountriesService(false);
            _outputHelper = testOutputHelper;
        }

        #region AddPerson

        [Fact]
        public void AddPerson_NullPerson()
        {
            PersonAddRequest? request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(request);
            });
        }

        [Fact]
        public void AddPerson_NullPersonName()
        {
            PersonAddRequest request = new PersonAddRequest
            {
                Name = null
            }
            ;

            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(request);
            });
        }

        [Fact]
        public void AddPerson_ValidPerson()
        {
            PersonAddRequest request = new PersonAddRequest
            {
                Name = "name",
                Email = "email@example.com",
                Address = "address",
                CountryId =  Guid.NewGuid(),
                DateOfBirth = DateTime.Now,
                Gender = GenderOptions.Male,
                ReceiveNewsLetter = true,
            };

            PersonResponse personResponse = _personService.AddPerson(request);
            List<PersonResponse> personResponses = _personService.GetAllPeople();
            
            Assert.True(personResponse.PersonId != Guid.Empty);
            Assert.Contains(personResponse, personResponses);
        }
        #endregion


        #region GetPersonByID
        [Fact]
        public void GetPersonByID_NullID()
        {
            Guid? guid = null;
            PersonResponse? response = _personService.GetPersonByID(guid);
            Assert.Null(response);
        }

        [Fact]
        public void GetPersonByID_ValidID()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Canada"};
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
            PersonAddRequest personAddRequest = new PersonAddRequest { 
                Name = "name", 
                Address = "address",
                DateOfBirth= DateTime.Now,
                Gender = GenderOptions.Male,
                ReceiveNewsLetter = true,
                Email = "email@example.com",
                CountryId = countryResponse.CountryId,

            };
            PersonResponse personResponse_Add = _personService.AddPerson(personAddRequest);
            PersonResponse? personResponse_Get = _personService.GetPersonByID(personResponse_Add.PersonId);

            Assert.Equal(personResponse_Add,personResponse_Get);
        }

        #endregion

        #region GetAllPeeple
        [Fact]
        public void GetAllPeople_EmptyList()
        {
            List<PersonResponse> personResponses = _personService.GetAllPeople();
            Assert.Empty(personResponses);
        }

        [Fact]
        public void GetAllPeople_AddFewPeople()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);


            _outputHelper.WriteLine("Expected:");
            foreach(PersonResponse personResponse in personResponses_Add)
            {
                _outputHelper.WriteLine(personResponse.ToString());  
            }

            List<PersonResponse> personResponses_Get = _personService.GetAllPeople();

            _outputHelper.WriteLine("Actual");
            foreach (PersonResponse personResponse in personResponses_Get)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }

            foreach (PersonResponse personResponse in personResponses_Get)
            {
                Assert.Contains(personResponse,personResponses_Add);
            }
        }

        #endregion

        #region GetFilteredPerson
        [Fact]
        public void GetFilterPerson_EmptySearchText()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse personResponse in personResponses_Add)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }

            List<PersonResponse> personResponses_Search = _personService.GetFilteredPerson(nameof(Person.Name),"");

            _outputHelper.WriteLine("Actual");
            foreach (PersonResponse personResponse in personResponses_Search)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }

            foreach (PersonResponse personResponse in personResponses_Search)
            {
                Assert.Contains(personResponse, personResponses_Add);
            }
        }

        [Fact]
        public void GetFilterPerson_SearchByName()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);
            
            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse personResponse in personResponses_Add)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }

            List<PersonResponse> personResponses_Search = _personService.GetFilteredPerson(nameof(Person.Name), "ma");

            _outputHelper.WriteLine("Actual");
            foreach (PersonResponse personResponse in personResponses_Search)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }

            foreach (PersonResponse personResponse in personResponses_Add)
            {
                if(personResponse.Name!= null)
                {
                    if (personResponse.Name.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponse, personResponses_Search);
                    }
                }
            }
        }

        #endregion

        #region GetSortedPeople
        [Fact]
        public void GetSortedPeople_PersonNameDesc()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);

            List<PersonResponse> allPeople = _personService.GetAllPeople();

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse personResponse in personResponses_Add)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }

            List<PersonResponse> personResponses_Sort = _personService.GetSortedPeople(allPeople, nameof(Person.Name),SortOrderOptions.Desc);

            _outputHelper.WriteLine("Actual");
            foreach (PersonResponse personResponse in personResponses_Sort)
            {
                _outputHelper.WriteLine(personResponse.ToString());
            }


            personResponses_Add = personResponses_Add.OrderByDescending(temp => temp.Name).ToList();

            for (int i = 0; i < personResponses_Add.Count; i++)
            {
                Assert.Equal(personResponses_Add[i],personResponses_Sort[i]);
            }

        }

        #endregion

        #region UpdatePerson
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest personUpdateRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_InvalidId()
        {
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() { PersonId = Guid.NewGuid()};

            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public void UpdatePerson_NullPersonName()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);

            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() { Name = null};

            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact] 
        public void UpdatePerson_NameAndEmail()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);

            PersonUpdateRequest personUpdateRequest = personResponses_Add.First().ToPersonUpdateRequest();

            personUpdateRequest.Email = "updated@email.com";
            personUpdateRequest.Name = "updated Name";

            PersonResponse updatePerson = _personService.UpdatePerson(personUpdateRequest);

            PersonResponse getPerson = _personService.GetFilteredPerson(nameof(PersonResponse.Name),"Updated Name" ).First();

            Assert.Equal(updatePerson, getPerson);            
        }

        #endregion

        #region DeletePerson
        [Fact]
        public void DeletePerson_ValidID()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);

            bool isDeleted = _personService.DeletePerson(personResponses_Add[0].PersonId);

            Assert.True(isDeleted);
        }

        [Fact]
        public void DeletePerson_InvalidID()
        {
            CountryResponse countryResponse1, countryResponse2;
            AddFewCountries(out countryResponse1, out countryResponse2);

            List<PersonResponse> personResponses_Add = AddFewPeople(countryResponse1, countryResponse2);

            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            Assert.False(isDeleted);
        }
        #endregion
        #region extraFunctions
        private List<PersonResponse> AddFewPeople(CountryResponse countryResponse1, CountryResponse countryResponse2)
        {
            PersonAddRequest personAddRequest1 = new PersonAddRequest
            {
                Address = "Address1",
                CountryId = countryResponse1.CountryId,
                DateOfBirth = DateTime.Now,
                Email = "email1@example.com",
                Gender = GenderOptions.Male,
                Name = "mac",
                ReceiveNewsLetter = true,
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest
            {
                Address = "Address2",
                CountryId = countryResponse1.CountryId,
                DateOfBirth = DateTime.Now,
                Email = "email2@example.com",
                Gender = GenderOptions.Male,
                Name = "mad",
                ReceiveNewsLetter = true,
            };

            PersonAddRequest personAddRequest3 = new PersonAddRequest
            {
                Address = "Address3",
                CountryId = countryResponse2.CountryId,
                DateOfBirth = DateTime.Now,
                Email = "email3@example.com",
                Gender = GenderOptions.Male,
                Name = "cad",
                ReceiveNewsLetter = false,
            };

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
            {
                personAddRequest1, personAddRequest2, personAddRequest3,
            };

            List<PersonResponse> personResponses_Add = new List<PersonResponse>();

            foreach (PersonAddRequest personAddRequest in personAddRequests)
            {
                personResponses_Add.Add(_personService.AddPerson(personAddRequest));
            }

            return personResponses_Add;
        }

        private void AddFewCountries(out CountryResponse countryResponse1, out CountryResponse countryResponse2)
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Egypt" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "USA" };

            countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
            countryResponse2 = _countriesService.AddCountry(countryAddRequest2);
        }
        #endregion

    }

}
