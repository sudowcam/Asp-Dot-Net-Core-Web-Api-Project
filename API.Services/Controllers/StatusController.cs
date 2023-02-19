using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;
using System.Net;
using Todo.API.Data.Entities;
using Todo.API.Utilities;

namespace Todo.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/Status")]
    public class StatusController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public StatusController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusList()
        {
            var model = await _repository.Status.GetStatusList();
            return model == null ? NotFound() : Ok(model);
        }
    }
}





