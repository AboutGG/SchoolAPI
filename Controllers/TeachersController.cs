using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.DTO;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeachersController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        #region Get all Teachers
        /// <summary> get call on student breakpoint </summary>
        /// <returns>All Teachers</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            return Ok(_mapper.Map<List<TeacherDTO>>(_teacherRepository.GetTeachers()));
        }
        #endregion

        #region Add a Teacher
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostStudents([FromBody] TeacherDTO newTeacher)
        {
            if (newTeacher == null)
                return BadRequest(ModelState);

            var teacher = new Teacher()
            {
                Id = new Guid(),
                RegistryId = newTeacher.RegistryId,
                UserId = newTeacher.UserId
            };

            if (!_teacherRepository.CreateTeacher(teacher))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500);
            }

            return Ok("succeffully created");
        }
        #endregion
    }
}
