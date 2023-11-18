


using Application.interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zDataApi.Controllers
{
    [Authorize]
    [Route("api/StolenProperties/")]
    [ApiController]

    public class StolenItemController : Controller
    {
        private readonly ICrimeAPIService _crimeAPIService;
        public StolenItemController(ICrimeAPIService crimeAPIService)
        {
            _crimeAPIService = crimeAPIService;

        }



        [HttpGet]
        [Route("returnstolenItem")]
        [ProducesResponseType(typeof(Application.Models.stolenItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> returnStolenItemnData()
        {
            try
            {


                var response = _crimeAPIService.returnStolenItem();



                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMesage = $"Could not get details for TRN: '{Response}'!";


                return StatusCode(StatusCodes.Status500InternalServerError, errorMesage);
            }
        }

        [HttpPost]
        [Route("addStolenItem")]
        [ProducesResponseType(typeof(Application.Models.stolenItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> addStolenItem(stolenItem item)
        {
            try
            {


                var response = _crimeAPIService.addStolenItem(item);

                //   _logger.LogInformation($"Details for TRN - '{request.Trn}': {Environment.NewLine}{response}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMesage = $"Could not get details for TRN: '{Response}'!";

                //    _logger.LogError(ex, errorMesage);

                return StatusCode(StatusCodes.Status500InternalServerError, errorMesage);
            }
        }

        [HttpDelete]
        [Route("removenStolenItem")]
        [ProducesResponseType(typeof(Application.Models.stolenItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> removenStolenItem(stolenItem item)
        {
            try
            {


                var response = _crimeAPIService.removeStolenItem(item);

                //   _logger.LogInformation($"Details for TRN - '{request.Trn}': {Environment.NewLine}{response}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMesage = $"Could not get details for TRN: '{Response}'!";

                //    _logger.LogError(ex, errorMesage);

                return StatusCode(StatusCodes.Status500InternalServerError, errorMesage);
            }
        }


    }
}
