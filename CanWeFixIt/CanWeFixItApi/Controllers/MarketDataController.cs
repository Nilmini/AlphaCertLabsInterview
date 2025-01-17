﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItApi.Constants;
using CanWeFixItApi.Models;
using CanWeFixItService;
using CanWeFixItService.Models;
using CanWeFixItService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CanWeFixItApi.Controllers
{
    /// <summary>
    /// Controller class MarketDataController
    /// Url path : ~/v1/marketdata
    /// </summary>
    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of the IMarketDataService
        /// </summary>
        /// <param name="marketDataService"> Instance of IMarketDataService</param>
        /// <param name="logger"> Instance of Logger</param>
        public MarketDataController(IMarketDataService marketDataService, ILogger<MarketDataController> logger)
        {
            _marketDataService = marketDataService;
            _logger = logger;
        }

        /// <summary>
        /// [GET]
        /// Get active MarketData which has sedol
        /// </summary>
        /// <returns>If success return OK response, else return Internal server error</returns>
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            try
            {
                _logger.LogInformation("MarketDataController.Get() called.");

                return Ok(_marketDataService.getActiveMarketDataWithSedol());
            }
            catch (Exception ex)
            {
                _logger.LogError("Message: " + ex.Message + Environment.NewLine + "Stack Trace: " + ex.StackTrace.ToString());

                return StatusCode(500, new ErrorResponse { message = ErrorMessages.CommonErrorMessage });
            }
        }
    }
}