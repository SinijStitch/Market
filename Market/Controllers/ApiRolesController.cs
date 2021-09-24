using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRolesController : ControllerBase
    {
        private readonly MarketContext _context;
        public ApiRolesController(MarketContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> res = new List<string>();
            foreach(Role r in _context.Roles)
            {
                res.Add(r.Name);
            }

            return res.ToArray();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
