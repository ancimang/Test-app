using AnaVeljovicTest.Interface;
using AnaVeljovicTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AnaVeljovicTest.Controllers
{
    public class AgentiController : ApiController
    {

        IAgent _repository { get; set; }

        public AgentiController(IAgent repository)
        {
            _repository = repository;
        }

        [Route("api/agenti")]
        public IEnumerable<Agent> Get()
        {
            return _repository.GetAll();
        }
        [Route("api/extremi")]
        public IEnumerable<Agent> GetExtremi()
        {
            return _repository.GetExtremi();
        }
        [Route("api/najmladji")]
        public IEnumerable<Agent> GetNajmladji()
        {
            return _repository.GetNajmladji();
        }
        public IHttpActionResult Get(int id)
        {
            var agent = _repository.GetById(id);
            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
        }
    }
}
