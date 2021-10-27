using Brainer.NetCore.Models;
using Brainer.NetCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //api/users

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await userRepository.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        //api/users/10

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var result = await userRepository.GetUser(id);
                if(result == null)
                {
                    return NotFound();
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }


        //api/users

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var userCheck = await userRepository.GetUserByEmail(user.Email);
                if(userCheck != null)
                {
                    ModelState.AddModelError("Email", "email already used");
                    return BadRequest();
                }
                var createdUser = await userRepository.AddUser(user);
                return CreatedAtAction(nameof(GetUser),
                    new {id = createdUser.Id}, createdUser);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        //api/users

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest("user id mismatch");

                var userToUpdate = await userRepository.GetUser(id);
                if (userToUpdate == null)
                {
                    return NotFound($"Employee with id = {id} not found");
                }

                return await userRepository.UpdateUser(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user with id = {user.Id}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> UpdateUser(int id)
        {
            try
            {
               
                var userToDelete = await userRepository.GetUser(id);
                if (userToDelete == null)
                {
                    return NotFound($"Employee with id = {id} not found");
                }

                await userRepository.DeleteUser(id);
                return Ok($"Employee with Id = {id} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting employee");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<User>>> Search(string email)
        {
            try
            {
                var result  = await userRepository.Search(email);
                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from database");
            }
        }
    }
}
