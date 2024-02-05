using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApiTest.Dto;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public StudentController(ApplicationContext context)
        {
            _context = context; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var entity = _context.Model.FindEntityType(typeof(Student).FullName);
            Console.WriteLine(entity);
            var tableName = entity.GetTableName();
            Console.WriteLine(tableName);
            var schemaName = entity.GetSchema();
            Console.WriteLine(schemaName);
            var key = entity.FindPrimaryKey();
            Console.WriteLine(key);
            var properties = entity.GetProperties();
            Console.WriteLine(properties);

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveStudent(StudentDto dto)
        {
            var student = new Student();
            student.StudentId = Guid.NewGuid();
            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Address = dto.Address;
            student.Age = dto.Age;

            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, StudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                student.Name = dto.Name;
                student.Email = dto.Email;
                student.Address = dto.Address;
                student.Age = dto.Age;
                await _context.SaveChangesAsync();
                return Ok(student);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Remove(student);
                _context.SaveChanges();
                return Ok(student);
            }
            return NotFound();
        }
    }
}
