using Microsoft.AspNetCore.Mvc;
using PetOwner.Models;
using PetOwner.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetOwner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipController : ControllerBase
    {
        private readonly ITipRepository _tipRepository;

        public TipController(ITipRepository tipRepository)
        {
            _tipRepository = tipRepository;
        }

        // GET: api/<TipController>
        [HttpGet]
        public ActionResult<List<Tip>> Get()
        {
            var tips = _tipRepository.GetAll();

            if (tips != null)
                return Ok(tips);

            return Ok("eroare");
        }

        // GET api/<TipController>/5
        [HttpGet("{id}")]
        public ActionResult<Tip> Get(int id)
        {
            return Ok(_tipRepository.Get(id));
        }

        [HttpGet("title/{title}")]
        public ActionResult<List<Tip>> GetByTitle(string title)
		{
            var tip = _tipRepository.GetByTitle(title);

            if (tip != null)
                return Ok(tip);

            return Ok("Not Found");
		}

        // POST api/<TipController>
        [HttpPost]
        public ActionResult Post([FromBody] Tip value)
        {
            _tipRepository.Insert(value);
            if (_tipRepository.Save())
			{
                var id = _tipRepository.GetByTitle(value.Title).FirstOrDefault().TipId;
                return Ok(new {tipid =  id });
            }
                

            return Ok("eroare");
        }

        // PUT api/<TipController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tip value)
        {
            var T = _tipRepository.Get(id);

            if (T == null) return Ok("eroare");

            if (value.Title != null) T.Title = value.Title;

            if (value.Description != null) T.Description = value.Description;

            if (value.Category != null) T.Category = value.Category;

            if (value.Race != null) T.Race = value.Race;

            if (value.Species != null) T.Species = value.Species;

            if (_tipRepository.Save()) return Ok();

            return Ok("eroare");

        }

        // DELETE api/<TipController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _tipRepository.Delete(_tipRepository.Get(id));

            if (_tipRepository.Save()) return Ok();

            return Ok("eroare");
        }
    }
}