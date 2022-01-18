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
    public class StudentsController : ControllerBase
    {
        StudentDbContext _context;
        public StudentsController(StudentDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _context.Students.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Student Get(int  id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public void Post(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, Student obj)
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
            }
            
        }
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
