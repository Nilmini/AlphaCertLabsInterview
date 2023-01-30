using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItApi.Constants;
using CanWeFixItApi.Models;
using CanWeFixItService;
using CanWeFixItService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CanWeFixItApi.Controllers
{
    /// <summary>
    /// Controller class ValuationController
    /// Url path : ~/v1/valuations
    /// </summary>
    [ApiController]
    [Route("v1/valuations")]
    public class ValuationController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of the ValuationController
        /// </summary>
        /// <param name="marketDataService"> Instance of IMarketDataService</param>
        /// <param name="logger"> Instance of Logger</param>
        public ValuationController(IMarketDataService marketDataService, ILogger<ValuationController> logger)
        {
            _marketDataService = marketDataService;
            _logger = logger;
        }

        /// <summary>
        /// [GET]
        /// Get market data valuation 
        /// </summary>
        /// <returns>If success return OK response, else return Internal server error</returns>
        public async Task<ActionResult<IEnumerable<MarketValuationDto>>> Get()
        {
            try
            {
                _logger.LogInformation("ValuationController.Get() called.");

                return Ok(_marketDataService.getActiveMarketDataValuation());
            }
            catch(Exception ex)
            {
                _logger.LogError("Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace.ToString());

                return StatusCode(500, new ErrorResponse { message = ErrorMessages.CommonErrorMessage });
            }
        }
    }
}

