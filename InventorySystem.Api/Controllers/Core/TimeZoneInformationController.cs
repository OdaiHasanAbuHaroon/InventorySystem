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
    /// Controller for managing time zone information.
    /// Provides APIs for retrieving and listing time zone data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeZoneInformationController : ControllerBaseExt
    {
        private readonly ILogger<TimeZoneInformationController> _logService;
        private readonly ITimeZoneInformationService _ITimeZoneInformationService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeZoneInformationController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for time zone information.</param>
        /// <param name="helperService">Helper service for additional utility operations.</param>
        public TimeZoneInformationController(ILogger<TimeZoneInformationController> logger, ITimeZoneInformationService service, IHelperService helperService)
        {
            _logService = logger;
            _ITimeZoneInformationService = service;
            _helpService = helperService;
        }

        /// <summary>
        /// Retrieves a list of time zone information based on the provided filter model.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching time zone information.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="TimeZoneInformationDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("List")]
        public async Task<GenericResponse<List<TimeZoneInformationDataModel>>> List(TimeZoneInformationFilterModel filterModel)
        {
            return await _ITimeZoneInformationService.TimeZoneInformationList(filterModel, GetLanguage());
        }

        /// <summary>
        /// Retrieves details of a specific time zone by its ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the time zone information.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="TimeZoneInformationDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet]
        public async Task<GenericResponse<TimeZoneInformationDataModel>> Get(long Id)
        {
            return await _ITimeZoneInformationService.GetTimeZoneInformation(Id, GetLanguage());
        }
    }
}
