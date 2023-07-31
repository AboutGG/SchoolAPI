using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.DTO;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IMapper mapper, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        #region Get all students
        /// <summary> get call on student breakpoint </summary>
        /// <returns>All students</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            return Ok(_mapper.Map<List<StudentDTO>>(_studentRepository.GetStudents()));
        }
        #endregion

        #region Add a Student
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostStudents([FromBody] StudentDTO newStudent)
        {
            if (newStudent == null)
                return BadRequest(ModelState);

            var student = new Student()
            {
                Id = new Guid(),
                Classroom = newStudent.Classroom,
                RegistryId = newStudent.RegistryId,
                UserId = newStudent.UserId
            };

            if (!_studentRepository.CreateStudent(student))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500);
            }

            return Ok("succeffully created");
        }
        #endregion
    }
}
