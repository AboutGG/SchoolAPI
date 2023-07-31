using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.DTO;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using SchoolAPI.Repository;
using SchoolAPI.Utils;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : Controller
    {
        #region Attributes
        private readonly IMapper _mapper;
        private readonly IExamRepository _examRepository;
        #endregion

        #region Costructor
        public ExamsController(
            IMapper mapper,
            IExamRepository examRepository
            )
        {
            this._mapper = mapper;
            this._examRepository = examRepository;
        }
        #endregion

        #region API calls

        #region Get Exams
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Exam>))]
        public IActionResult GetExams()
        {
            return Ok(_mapper.Map<List<ExamDTO>>(_examRepository.GetExams()));
        }

        #endregion

        #region Post Exam
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateExams([FromBody] ExamDTO newExam)
        {
            if (newExam == null)
            {
                return BadRequest(ModelState);
            }
            var exam = new Exam
            {
                Id = new Guid(),
                SubjectId = newExam.SubjectId,
                ExamDate = newExam.ExamDate
            };

            if (!this._examRepository.CreateExam(exam))
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
