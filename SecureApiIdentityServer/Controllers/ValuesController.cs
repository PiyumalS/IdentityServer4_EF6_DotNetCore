using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccess.DataAccess;
using DataAccess.IdentiyModels;
using Business;
using Domain;

namespace SecureApiIdentityServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IUserManager _userManager;
        public ValuesController(IUserManager userManager)
        {


            _userManager = userManager;

        }

        // GET api/values
        //[Authorize("permission1")]
        [HttpGet]
        public IEnumerable<string> Get()
        {

            UserDTO nwDTO = new UserDTO();
            nwDTO.Email = "nile@gmail.com";
            nwDTO.IsTempararyPassword = true;
            nwDTO.NationalID = "8857815142V";
            nwDTO.IsFirstAttempt = true;
            nwDTO.PhoneNumber = "123123123";
            nwDTO.LockoutEnabled = false;
            nwDTO.Password = "12";

            _userManager.CreateUserAsync(nwDTO);

            RoleDTO roleDTO = new RoleDTO();
            roleDTO.Name = "Admin";

            _userManager.CreateRoleAsync(roleDTO);

            return new string[] { "value1", "value2" };


        }

        // GET api/values/5
        // [Authorize("permission2")]
        [HttpGet("{id}")]
        public string Get(int id)
        {

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
