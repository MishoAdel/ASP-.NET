
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;

namespace ServiceContracts
{
    public interface IPersonService
    {
        /// <summary>
        /// Adds person to list of people
        /// </summary>
        /// <param name="request">PersonRequest to add</param>
        /// <returns>Person Response object with newly generated Id</returns>
        PersonResponse AddPerson (PersonAddRequest request);

        /// <summary>
        /// Get all people in list
        /// </summary>
        /// <returns>List of PersonResponse</returns>
        List<PersonResponse> GetAllPeople();
    
        /// <summary>
        /// Get a person by ID
        /// </summary>
        /// <param name="personID">Id of the person</param>
        /// <returns>Returns person that matches the id</returns>
        PersonResponse? GetPersonByID (Guid? personID);

        /// <summary>
        /// Searches people using a parameter 
        /// </summary>
        /// <param name="searchBy">the parameter used for filtering</param>
        /// <param name="searchString">the string used for searching with</param>
        /// <returns>Returns list of filtered people</returns>
        List<PersonResponse> GetFilteredPerson(string searchBy, string searchString);

        /// <summary>
        /// Returns a list of ordered PersonResponse based on order parameter
        /// </summary>
        /// <param name="allPeople">List to get ordered</param>
        /// <param name="sortBy">Sort parameter to sort with</param>
        /// <param name="sortOrder">Asc od Desc</param>
        /// <returns>Ordered list</returns>
        List<PersonResponse> GetSortedPeople(List<PersonResponse> allPeople, string sortBy,SortOrderOptions sortOrder);
    
        /// <summary>
        /// update person info
        /// </summary>
        /// <param name="request">Update request</param>
        /// <returns>PersonResponse with updated data</returns>
        PersonResponse? UpdatePerson (PersonUpdateRequest? request);

        /// <summary>
        /// Deletes a person
        /// </summary>
        /// <param name="personId">Id of person yuo want to delete</param>
        /// <returns>Boolean determines whether the delete was s or not</returns>
        bool DeletePerson(Guid personId);
    }
}
