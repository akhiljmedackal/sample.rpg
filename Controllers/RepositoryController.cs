using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sample.rpg.Dto.User;

namespace sample.rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryServices _repository;

        public RepositoryController(IRepositoryServices repository)
        {
            _repository = repository;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>>UserRegisteration(UserDto request)
        {
            var demo = new User {UserName=request.UserName};
            var response= await _repository.UserRegisteration(demo,request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>>Login(UserLoginDto r)
        {
            var response=await _repository.Login(r.UserName,r.Password);
            return Ok(response);
        }
    }
}