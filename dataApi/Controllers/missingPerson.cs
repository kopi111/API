using Application.interfaces;
using Application.Looger;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace zDataApi.Controllers
{
    //[Authorize]
    [Route("api/Missing/")]
    [ApiController]
    public class MissingPersonController : Controller
    {


        private readonly ICrimeAPIService _crimeAPIService;
        private readonly ILogger<MissingPersonController> _logger;

        public MissingPersonController(ICrimeAPIService crimeAPIService, ILogger<MissingPersonController> logger)
        {
            _crimeAPIService = crimeAPIService ?? throw new ArgumentNullException(nameof(crimeAPIService));
            _logger = logger ?? throw new ArgumentNullException();

        }

        [HttpGet]
        [Route("returnMissingPersonData")]
        [ProducesResponseType(typeof(missingPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> returnMissingPersonData()
        {
            try
            {


               // var response = _crimeAPIService.returnMissingPersonData();



                return Ok("good");
            }
            catch (Exception ex)
            {
                var errorMesage = $"Could not get details for TRN: '{Response}'!";


                return StatusCode(StatusCodes.Status500InternalServerError, errorMesage);
            }
        }

        [HttpPost]
        [Route("addMissingPersonData")]
        [ProducesResponseType(typeof(missingPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> addMissingPersonData([FromForm] missingPerson person)
        {

            try
            {
                if (person == null)
                {
                    _logger.LogError($" Please check request");

                    return BadRequest();
                }
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string path = Path.Combine(desktopPath, "pictures");
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    person.ImagePath.formFile.CopyTo(stream);
                }
                var data = await _crimeAPIService.addMissingPerson(person);
               
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to log Customer Information");

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not facilitate request at this time. Please try again later");
            }
           
        }

        [HttpDelete]
        [Route("removeMissingPersonData")]
        [ProducesResponseType(typeof(missingPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> removenMissingPersonData(missingPerson person)
        {
            try
            {


                var response = _crimeAPIService.removeMissingPerson(person);

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

        [HttpPut]
        [Route("updateMissingPersonData")]
        [ProducesResponseType(typeof(missingPerson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> updateMissingPersonData(missingPerson person)
        {
            try
            {


                var response = _crimeAPIService.updateMissingPerson(person);

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
