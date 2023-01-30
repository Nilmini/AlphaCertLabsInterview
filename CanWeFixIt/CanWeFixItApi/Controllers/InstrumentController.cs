using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItApi.Models;
using CanWeFixItApi.Constants;
using CanWeFixItService.Models;
using CanWeFixItService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CanWeFixItApi.Controllers
{
    /// <summary>
    /// Controller class InstrumentController
    /// Url path : ~/v1/instruments
    /// </summary>
    [ApiController]
    [Route("v1/instruments")]
    public class InstrumentController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of the InstrumentController
        /// </summary>
        /// <param name="instrumentService"> Instance of IInstrumentService</param>
        /// <param name="logger"> Instance of Logger</param>
        public InstrumentController(IInstrumentService instrumentService, ILogger<InstrumentController> logger)
        {
            _instrumentService = instrumentService;
            _logger = logger;
        }

        /// <summary>
        /// [GET]
        /// Get Active instruments
        /// </summary>
        /// <returns>If success return OK response, else return Internal server error</returns>
        public async Task<ActionResult<IEnumerable<InstrumentDto>>> Get()
        {
            try
            {
                _logger.LogInformation("InstrumentController.Get() called.");

                return Ok(_instrumentService.getActiveInstruments());
            }
            catch(Exception ex)
            {
                _logger.LogError("Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace.ToString());

                return StatusCode(500, new ErrorResponse { message = ErrorMessages.CommonErrorMessage });
            }
        }
    }
}