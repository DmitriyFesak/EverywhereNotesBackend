﻿using EverywhereNotes.Database;
using EverywhereNotes.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EverywhereNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private DataContext _dataContext;

        public NotesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: api/<NotesController>
        [HttpGet]
        public IActionResult Get()
        {
            var notes = _dataContext.Notes.ToList();
            
            return Ok(notes);
        }

        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotesController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            _dataContext.Add(new Note
            {
                Title = value,
                Content = "zxc",
                CreationDateTime = DateTime.Now
            });
            var saved = _dataContext.SaveChanges();

            return Ok(saved > 0);
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}