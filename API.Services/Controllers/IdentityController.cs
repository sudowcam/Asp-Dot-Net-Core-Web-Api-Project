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
    [Route("api/Identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public IdentityController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var model = await _repository.Users.GetUserList();
            return model == null ? NotFound() : Ok(model);
        }
    }
}