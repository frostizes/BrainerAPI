using Brainer.NetCore.Models.Entities;
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
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUsersController(IApplicationUserRepository applicationUserRepository)
        {
            this._applicationUserRepository = applicationUserRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await _applicationUserRepository.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        //{
        //    try
        //    {
        //        var result = await _applicationUserRepository.GetUser(id);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error");
        //    }
        //}

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Search(string email)
        {
            try
            {
                var result = await _applicationUserRepository.GetUserByEmail(email);
                if (result != null)
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
