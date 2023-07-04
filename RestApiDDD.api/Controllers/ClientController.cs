using Microsoft.AspNetCore.Mvc;
using RestApiDDD.Application.DTOs.Request;
using RestApiDDD.Application.DTOs.Request.Update;
using RestApiDDD.Application.DTOs.Response;
using RestApiDDD.Application.Interfaces;

namespace RestApiDDD.Api.Controllers
{
    [ApiController]
    [Route("/client/v1")]
    public class ClientController : Controller
    {
        private readonly IApplicationServiceClient _applicationServiceClient;

        public ClientController(IApplicationServiceClient applicationServiceClient)
        {
            _applicationServiceClient = applicationServiceClient;
        }
      
        [HttpPost]
        [Route("/register-client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClient([FromBody] ClientRequestDTO clientDto) 
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceClient.Add(clientDto);

                if (response.Success)
                    return Created("Successufully registered.", response);
                
                return BadRequest(response);
            }
            
            return StatusCode(500);
        }

        [HttpGet]
        [Route("/show-all-clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllClient()
        {
            try
            {
                var response =  await _applicationServiceClient.GetAll();

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/show-one-client/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneClient(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceClient.GetById(id);

                if(response.Success)
                    return Ok(response);

                return BadRequest(response);
            }

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("/delete-client/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceClient.Delete(id);
                
                if(response.Success)
                    return Ok(response);
                
                return BadRequest(response);
            }

            return StatusCode(500);
        }

        [HttpPut]
        [Route("/update-client/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClient(Guid id, 
                                         [FromBody] ClientRequestUpdateDTO clientDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _applicationServiceClient.Update(id, clientDto);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);
            }

            return StatusCode(500);
        }
    }
}