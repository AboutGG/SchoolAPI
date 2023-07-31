using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.DTO;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using SchoolAPI.Utils;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IRegistryRepository _registryRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;

        #region Constructor
        public UsersController(
            ITransactionRepository transactionRepository,
            IUserRepository userRepository,
            ITeacherRepository teacherRepository,
            IRegistryRepository registryRepository,
            IStudentRepository studentRepository,
            IMapper mapper
            )
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._transactionRepository = transactionRepository;
            this._teacherRepository = teacherRepository;
            this._registryRepository = registryRepository;
            this._studentRepository = studentRepository;
        }
        #endregion

        #region Get all users
        /// <summary> get call on user breakpoint </summary>
        /// <returns>All User</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            return Ok(_mapper.Map<List<UserDTO>>(_userRepository.GetUsers()) );
        }
        #endregion

        #region Get user on pdf
        [HttpGet("users_pdf")]
        public IActionResult GetUsersOnPdf()
        {
            ///<summary>We return a Bytes array because the PDF is a sequence of binary bytes to represent the document content compactly. </summary>
            byte[] pdfBytes = PdfGenerator.GeneratePdf(_mapper.Map<List<UserDTO>>(_userRepository.GetUsers()));

            // Returns the PDF
            return File(pdfBytes, "application/pdf", "my_document.pdf");
        }
        #endregion

        #region Get user by id
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(Guid id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();
            var user = _mapper.Map<UserDTO>(_userRepository.GetUserById(id));
            return Ok(user);
        }
        #endregion

        #region Add an user
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostUser([FromBody] UserDTO newUser)
        {
            if(newUser == null)
                return BadRequest(ModelState);

            ///<summary> check if user exist using the username</summary>
            if (_userRepository.UserExists(newUser.Username))
            {
                ModelState.AddModelError("response", "user already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User()
            {
                Id = new Guid(),
                Username = newUser.Username,
                Password = newUser.Password,
            };

            if (!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500);
            }

            return Ok("succeffully created");
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// delete a user giving me an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A string with user deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(Guid id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var userToDelete = _userRepository.GetUserById(id);

            if (_userRepository.DeleteUser(userToDelete))
                ModelState.AddModelError("", "something wrong while deleting the user");
            return Ok("User deleted");
        }
        #endregion

        #region Put an user
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        ///<summary> update user using the id</summary>
        public IActionResult UpdateUser(Guid userId, [FromBody] UserDTO updatedUser)
        {
            Console.WriteLine();
            //if updatedUser is null it returns bad request
            if (updatedUser == null)
            {
                Console.WriteLine("è null");
                return BadRequest(ModelState);

            }
            //if taken is null return not found
            var takenUser = _userRepository.GetUserById(userId);
            if (takenUser == null)
            {
                Console.WriteLine("non trovato");
                return NotFound();
            }
            takenUser.Username = updatedUser.Username ?? takenUser.Username;
            takenUser.Password = updatedUser.Password ?? takenUser.Password;

            if (!_userRepository.UpdateUser(takenUser))
            {
                ModelState.AddModelError("response", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion

        #region Add an entity

        [HttpPost("{test}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddEntity(EntityDTO entity)
        {
            if (entity == null)
            {
                return BadRequest(ModelState);
            }
            else if ((entity.Classroom != null && !entity.isStudent) || (entity.Classroom == null && entity.isStudent))
            {
        
                ModelState.AddModelError("Error response", "Classroom error, you've passed a classroom on Teacher/ you dont passed classroom on Student");
                return BadRequest(ModelState);
            }

            if (_userRepository.UserExists(entity.User.Username))
            {
                ModelState.AddModelError("response", "user already exist");
                return StatusCode(422, ModelState);
            }

            ///<summary> start transaction </summary>
            var transaction = _transactionRepository.BeginTransaction();

            ///<summary> Create the user to add on db, taking the attributes from entity</summary>
            var user = new User
            {
                Id = new Guid(),
                Username = entity.User.Username,
                Password = entity.User.Password,
            };

            ///<summary> Create the Registry to add on db, taking the attributes from entity</summary>
            var registry = new Registry
            {
                Id = new Guid(),
                Name = entity.Registry.Name,
                Surname = entity.Registry.Surname,
                Birth = entity.Registry.Birth
            };

            ///<summary> Try to create an user and registry</summary>
            if (_userRepository.CreateUser(user) && _registryRepository.CreateRegistry(registry))
            {
                if (entity.isStudent)
                {

                    var student = new Student
                    {
                        Id = new Guid(),
                        UserId = user.Id,
                        RegistryId = registry.Id,
                        Classroom = entity.Classroom,
                    };
                    if (_studentRepository.CreateStudent(student))
                    {
                        _transactionRepository.CommitTransaction(transaction);
                        return Ok("Student successfully created");
                    }
                    else
                    {
                        _transactionRepository.RollbackTransaction(transaction);
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    var teacher = new Teacher
                    {
                        Id = new Guid(),
                        UserId = user.Id,
                        RegistryId = registry.Id,
                    };

                    if (this._teacherRepository.CreateTeacher(teacher))
                    {
                        _transactionRepository.CommitTransaction(transaction);
                        return Ok("Successfully teacher created");

                    }
                    else
                    {
                        _transactionRepository.RollbackTransaction(transaction);
                        return BadRequest(ModelState);
                    }

                }
            }
            else
            {
                _transactionRepository.RollbackTransaction(transaction);
                ModelState.AddModelError("Response", "Teacher and student are nulls");
                return BadRequest(ModelState);
            }
        }
        #endregion

    }
}
