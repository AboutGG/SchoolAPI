using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.DTO;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectsController : Controller
    {
        #region Attributes
        private readonly IMapper _mapper;
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;
        #endregion

        #region Costructor
        public TeacherSubjectsController(
            IMapper mapper,
            ITeacherSubjectRepository teacherSubjectRepository
            )
        {
            this._mapper = mapper;
            this._teacherSubjectRepository = teacherSubjectRepository;
        }
        #endregion

        #region API calls

        #region GetTeacherSubjects
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherSubject>))]
        public IActionResult GetTeachersSubjects ()
        {
            return Ok(_mapper.Map<List<TeacherSubjectDTO>>(_teacherSubjectRepository.GetTeachersSubjects()));
        }
        #endregion

        #region SetTeacherSubjects
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult PostTeacherSubject([FromBody] TeacherSubjectDTO newTeacherSubject)
        {
            if (newTeacherSubject == null)
            {
                return BadRequest(ModelState);
            }
            var teacherSubject = new TeacherSubject
            {
                TeacherId = newTeacherSubject.TeacherId,
                SubjectId = newTeacherSubject.SubjectId,
                Classroom = newTeacherSubject.Classroom
            };

            if (!this._teacherSubjectRepository.CreateTeacherSubject(teacherSubject))
            {
                ModelState.AddModelError("Error response", "Something went wrong while savin");
                return StatusCode(500);
            }

            return Ok("Succesfully created.");
        }
        #endregion
        #endregion
    }
}
