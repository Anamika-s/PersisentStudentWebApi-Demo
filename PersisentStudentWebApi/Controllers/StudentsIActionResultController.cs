using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersisentStudentWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersisentStudentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Students1Controller : ControllerBase
    {
        StudentDbContext _context;
        public Students1Controller(StudentDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult Get()
        {
            if (_context.Students.ToList().Count == 0)
                return NotFound();
            else
                return Ok(_context.Students.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int  id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
                return Ok(student);
            else
                return NotFound();
        }

        [HttpPost]
        
        public IActionResult Post(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return Created("Record Added", student);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, Student obj)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                foreach (var temp in _context.Students)
                {
                    if (temp.Id == id)
                    {
                        temp.Batch = obj.Batch;
                        temp.Marks = obj.Marks;
                       
                        break;
                    }
                   
                }
                _context.SaveChanges();
                return Ok("Record Edited");
            }
            else
                return NotFound();
            }
            
         
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return Ok("Record deleted");
            }
            else
                return NotFound();
        }
    }
}
