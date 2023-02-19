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
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var model = await _repository.Users.GetUserList();
            return model == null ? NotFound() : Ok(model);
        }
    }
}