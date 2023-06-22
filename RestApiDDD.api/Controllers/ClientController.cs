﻿using Microsoft.AspNetCore.Mvc;
using RestApiDDD.Application.DTOs;
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
        public ActionResult AddClient([FromBody] ClientDto clientDto) 
        {
            _applicationServiceClient.Add(clientDto);
            
            return Ok("Client added successfully.");
        }

        [HttpGet]
        [Route("/show-all-clients")]
        public ActionResult<IEnumerable<ClientDto>> GetAllClient()
        {
            return Ok(_applicationServiceClient.GetAll());
        }

        [HttpGet]
        [Route("/show-one-client/{id}")]
        public ActionResult<ClientDto> GetOneClient(Guid id)
        {
            var client = _applicationServiceClient.GetById(id);
            
            if(client == null)
                return NotFound("Client by Id not found.");

            return client;
        }

        [HttpDelete]
        [Route("/delete-client/{id}")]
        public ActionResult DeleteClient(Guid id)
        {
            var client = _applicationServiceClient.GetById(id);

            if (client == null)
                return NotFound("Client by Id not found.");

            _applicationServiceClient.Delete(client);

            return Ok("Client deleted successfully.");
        }

        [HttpPut]
        [Route("/update-client/{id}")]
        public ActionResult UpdateClient(Guid id, 
                                         [FromBody] ClientDto clientDto)
        {
            var client = _applicationServiceClient.GetById(id);

            if (client == null)
                return NotFound("Client by Id not found.");

            _applicationServiceClient.Update(clientDto);

            return Ok("Client updating successfully.");
        }
    }
}