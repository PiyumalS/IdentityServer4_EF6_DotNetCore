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
using SecureApiIdentityServer.AuthorizationHandler;

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
        [Authorize]
        [RequiresPermission("p1")]
        public IEnumerable<string> Get()
        {
            try
            {
                RoleDTO roleDTO = new RoleDTO();
                roleDTO.Name = "Admin1";

                //_userManager.CreateRoleAsync(roleDTO);

                try
                {
                    UserDTO nwDTO = new UserDTO();
                    nwDTO.Email = "nile@gmail.com";
                    nwDTO.IsTempararyPassword = true;
                    nwDTO.NationalID = "8857815122V";
                    nwDTO.IsFirstAttempt = true;
                    nwDTO.PhoneNumber = "123123123";
                    nwDTO.LockoutEnabled = false;
                    nwDTO.Password = "123456";

                    //  _userManager.CreateUserAsync(nwDTO, new string[] { roleDTO.Name });



                    return new string[] { "value1", "value2" };

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        // GET api/values/5
        [Authorize("permission3")]
        [HttpGet("{id}")]
       // [Authorize]
        [RequiresPermission("p3")]
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
