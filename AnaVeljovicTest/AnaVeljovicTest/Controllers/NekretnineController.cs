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
    public class NekretnineController : ApiController
    {
        INekretnina _repository { get; set; }

        public NekretnineController(INekretnina repository)
        {
            _repository = repository;
        }

        [Route("api/nekretnine")]
        public IEnumerable<Nekretnina> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            return Ok(nekretnina);
        }
       
        public IHttpActionResult Post(Nekretnina nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(nekretnina);

            return CreatedAtRoute("DefaultApi", new { id = nekretnina.Id }, nekretnina);
        }
        
        public IHttpActionResult Put(int id, Nekretnina nekretnina)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nekretnina.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(nekretnina);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(nekretnina);
        }
       
        public IHttpActionResult Delete(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            _repository.Delete(nekretnina);

            return StatusCode(HttpStatusCode.NoContent);

        }

        [Route("api/pretraga")]
        public IEnumerable<Nekretnina> PostPretraga(int x, int y)
        {
            return _repository.PostPretraga(x, y);
        }

        
        public IEnumerable<Nekretnina> GetNapravljeno(int napravljeno)
        {
            return _repository.GetNapravljeno(napravljeno);
        }





    }
}
