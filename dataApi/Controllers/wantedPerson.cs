using Application.interfaces;
using Application.Models;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zDataApi.Controllers
{

    [Authorize]
    [Route("api/Wanted/")]
    [ApiController]


    public class WantedPersonController : Controller
    {

       
        private readonly ICrimeAPIService _crimeAPIService;
        public WantedPersonController(ICrimeAPIService crimeAPIService)
        {
            _crimeAPIService = crimeAPIService;
           
        }



        [HttpGet]
        [Route("returnWantedPersonData")]
        [ProducesResponseType(typeof(Application.Models.wantedPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> returnWantedPersonData()
        {
            try
            {
              

               var response =  _crimeAPIService.returnWantedPersonData;

       

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorMesage = $"Could not get details for TRN: '{Response}'!";


                return StatusCode(StatusCodes.Status500InternalServerError, errorMesage);
            }
        }

        [HttpPost]
        [Route("addWantedPersonData")]
        [ProducesResponseType(typeof(Application.Models.wantedPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> addWantedPersonData(wantedPerson person)
        {
            try
            {


                var response = _crimeAPIService.addWantedPersonData(person);

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
        [Route("removeWantedPersonData")]
        [ProducesResponseType(typeof(Application.Models.wantedPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> removenWantedPersonData(Application.Models.wantedPerson person)
        {
            try
            {


                var response = _crimeAPIService.removeWantedPersonData(person);

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

