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
        [HttpGet]
        [Authorize]
        [RequiresPermission("ViewUsers")]
        public IEnumerable<string> Get()
        {
            List<string> colors = new List<string>();
            colors.Add("Red");
            colors.Add("Blue");
            colors.Add("Green");
            return colors;
        }

        [HttpGet("GetAuthOnly")]
        [Authorize]
        public IEnumerable<string> GetAuthOnly()
        {
            List<string> numbers = new List<string>();
            numbers.Add("1");
            numbers.Add("2");
            numbers.Add("3");
            return numbers;
        }

        [HttpGet("GetCustomPermission")]
        [RequiresPermission("ViewUsers")]
        public IEnumerable<string> GetCustomPermission()
        {
            List<string> names = new List<string>();
            names.Add("Kamal");
            names.Add("Sunil");
            names.Add("Amal");
            return names;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        // [Authorize]
        [RequiresPermission("p3,p2")]
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
