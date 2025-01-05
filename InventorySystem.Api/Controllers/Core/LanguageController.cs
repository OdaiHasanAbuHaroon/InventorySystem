using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Responses;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Core
{
    /// <summary>
    /// Controller for managing languages in the system.
    /// Provides APIs for retrieving a list of languages and specific language details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LanguageController : ControllerBaseExt
    {
        private readonly ILogger<LanguageController> _logService;
        private readonly ILanguageService _ILanguageService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for language-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public LanguageController(ILogger<LanguageController> logger, ILanguageService service, IHelperService helperService)
        {
            _logService = logger;
            _ILanguageService = service;
            _helpService = helperService;
        }

        /// <summary>
        /// Retrieves a list of languages based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching languages.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="LanguageDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("List")]
        public async Task<GenericResponse<List<LanguageDataModel>>> List(LanguageFilterModel filterModel)
        {
            return await _ILanguageService.LanguageList(filterModel, GetLanguage());
        }

        /// <summary>
        /// Retrieves details of a specific language by its ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the language.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="LanguageDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet]
        public async Task<GenericResponse<LanguageDataModel>> Get(long Id)
        {
            return await _ILanguageService.GetLanguage(Id, GetLanguage());
        }
    }
}
