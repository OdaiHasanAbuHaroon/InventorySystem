using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Core
{
    /// <summary>
    /// Controller for managing person-related operations such as listing, fetching, creating, updating, and deleting persons.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBaseExt
    {
        private readonly ILogger<PersonController> _logService;
        private readonly IPersonService _personService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for person-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public PersonController(ILogger<PersonController> logger, IPersonService service, IHelperService helperService)
        {
            _logService = logger;
            _personService = service;
            _helpService = helperService;
        }

        /// <summary>
        /// Retrieves a list of persons based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching persons.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="PersonDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("PersonList")]
        public async Task<GenericResponse<List<PersonDataModel>>> PersonList(PersonFilterModel filterModel)
        {
            try
            {
                return await _personService.PersonList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<PersonDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific person by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the person.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="PersonDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetPerson")]
        public async Task<GenericResponse<PersonDataModel>> GetPerson(long Id)
        {
            try
            {
                return await _personService.GetPerson(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the person to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeletePerson")]
        public async Task<GenericResponse<bool>> DeletePerson(long Id)
        {
            try
            {
                return await _personService.DeletePerson(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific person.
        /// </summary>
        /// <param name="id">The unique identifier of the person to be updated.</param>
        /// <param name="form">The updated person details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="PersonDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdatePerson")]
        public async Task<GenericResponse<PersonDataModel>> UpdatePerson(long id, PersonFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _personService.UpdatePerson(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new person using the provided form model.
        /// </summary>
        /// <param name="form">The person details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="PersonDataModel"/>.
        /// </returns>
        [HttpPost("CreatePerson")]
        public async Task<GenericResponse<PersonDataModel>> CreatePerson(PersonFormModel form)
        {
            try
            {
                return await _personService.CreatePerson(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
