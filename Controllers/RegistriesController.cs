using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.DTO;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;


namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistriesController : Controller
    {
        private readonly IRegistryRepository _registryRepository;
        private readonly IMapper _mapper;

        #region Constructor
        public RegistriesController(IRegistryRepository registryRepository, IMapper mapper)
        {
            this._registryRepository = registryRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Get Registries
        /// <summary> get call on Registry breakpoint </summary>
        /// <returns>All Registry</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Registry>))]
        public IActionResult GetRegistries([FromQuery] string? name, [FromQuery] string? classroom)
        {
            ///<summary> filter the element by name</summary>
            if (name != null)
            {
                var dummy = _mapper.Map<List<RegistryDTO>>(_registryRepository.GetRegistries(name));
                return dummy.Count > 0 ? Ok(dummy) : NotFound();
            }
            ///<summary> filter the element by classroom, it divides the teacher and studets</summary>
            if (classroom != null)
            {
                var dummy = _registryRepository.GetClassroom(classroom.Trim().ToLower());
                return dummy.Count > 0 ? Ok(dummy) : NotFound();
            }
            ///<summary> Return all the element when there aren't query params</summary>
            return Ok(_mapper.Map<List<RegistryDTO>>(_registryRepository.GetRegistries()));
        }
        #endregion

        #region Get Registry by id
        /// <summary> Get the element by id </summary>
        /// <param name="id"></param>
        /// <returns> A list of Registries filtered by name</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Registry))]
        [ProducesResponseType(400)]
        public IActionResult GetRegistryById(Guid id)
        {
            if (!_registryRepository.RegistryExists(id))
                return NotFound();
            var Registry = _mapper.Map<RegistryDTO>(_registryRepository.GetRegistryById(id));
            return Ok(Registry);
        }
        #endregion

        #region Add an Registry
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostRegistry([FromBody] RegistryDTO newRegistry)
        {
            if (newRegistry == null)
                return BadRequest(ModelState);

            ///<summary> check if Registry exist using the Registryname</summary>
            if (_registryRepository.GetRegistryById(newRegistry.Id) != null)
            {
                ModelState.AddModelError("response", "Registry already exist");
                return StatusCode(422, ModelState);
            }

            var Registry = new Registry()
            {
                Id = new Guid(),
                Name = newRegistry.Name,
                Surname = newRegistry.Surname,
                Birth = newRegistry.Birth
            };

            if (!_registryRepository.CreateRegistry(Registry))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500);
            }

            return Ok("succeffully created");
        }
        #endregion

        #region DeleteRegistry
        /// <summary>
        /// delete a Registry giving me an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A string with Registry deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRegistry(Guid id)
        {
            if (!_registryRepository.RegistryExists(id))
                return NotFound();

            var RegistryToDelete = _registryRepository.GetRegistryById(id);

            if (_registryRepository.DeleteRegistry(RegistryToDelete))
                ModelState.AddModelError("", "something wrong while deleting the Registry");
            return Ok("Registry deleted");
        }
        #endregion

        #region Put and Registry
        [HttpPut("{RegistryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        ///<summary> update Registry using the id</summary>
        public IActionResult UpdateRegistry(Guid RegistryId, [FromBody] RegistryDTO updatedRegistry)
        {
            Console.WriteLine();
            //if updatedRegistry is null it returns bad request
            if (updatedRegistry == null)
            {
                Console.WriteLine("è null");
                return BadRequest(ModelState);

            }
            //if taken is null return not found
            var takenRegistry = _registryRepository.GetRegistryById(RegistryId);
            if (takenRegistry == null)
            {
                Console.WriteLine("non trovato");
                return NotFound();
            }
            takenRegistry.Name = updatedRegistry.Name ?? takenRegistry.Name;
            takenRegistry.Surname = updatedRegistry.Surname ?? takenRegistry.Surname;
            takenRegistry.Birth = updatedRegistry.Birth;

            if (!_registryRepository.UpdateRegistry(takenRegistry))
            {
                ModelState.AddModelError("response", "Something went wrong updating Registry");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion

    }
}

