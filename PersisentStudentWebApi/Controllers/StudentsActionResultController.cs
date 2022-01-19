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
    public class Students2Controller : ControllerBase
    {
        StudentDbContext _context;
        public Students2Controller(StudentDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ActionResult<List<Student>> Get()
        {if (_context.Students.ToList().Count == 0)
                return NotFound();
        else 
            return _context.Students.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Student> Get(int  id)
        {

            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                return student;
            }
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Student> Post(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return student;
            }
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<string> Put(int id, Student obj)
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
                    _context.SaveChanges();

                }
                return "Edited";

            }
            else
                return NotFound();
            
        }
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<String> Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();

             
            return "Edited";

            }
            else
                return NotFound();

    }
}
}
