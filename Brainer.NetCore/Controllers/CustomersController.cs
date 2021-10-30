using Brainer.NetCore.Models;
using Brainer.NetCore.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        //api/users

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await customerRepository.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        //api/users/10

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetUser(int id)
        {
            try
            {
                var result = await customerRepository.GetUser(id);
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
        public async Task<ActionResult<Customer>> CreateUser(Customer user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var userCheck = await customerRepository.GetUserByEmail(user.Email);
                if(userCheck != null)
                {
                    ModelState.AddModelError("Email", "email already used");
                    return BadRequest();
                }
                var createdUser = await customerRepository.AddUser(user);
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
        public async Task<ActionResult<Customer>> UpdateUser(int id, Customer user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest("user id mismatch");

                var userToUpdate = await customerRepository.GetUser(id);
                if (userToUpdate == null)
                {
                    return NotFound($"Employee with id = {id} not found");
                }

                return await customerRepository.UpdateUser(user);
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
               
                var userToDelete = await customerRepository.GetUser(id);
                if (userToDelete == null)
                {
                    return NotFound($"Employee with id = {id} not found");
                }

                await customerRepository.DeleteUser(id);
                return Ok($"Employee with Id = {id} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting employee");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Customer>>> Search(string email)
        {
            try
            {
                var result  = await customerRepository.Search(email);
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
