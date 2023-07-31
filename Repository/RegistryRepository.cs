using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Repository
{
    public class RegistryRepository : IRegistryRepository
    {
        #region Attributes
        private readonly SchoolContext _context;
        #endregion

        #region Costructor
        public RegistryRepository(SchoolContext context)
        {
            this._context = context;
        }
        #endregion

        #region Interfaces


        /// <summary> Get all the Registrys </summary>
        public ICollection<Registry> GetRegistries()
        {
            return _context.Registries.OrderBy(u => u.Id).ToList();
        }

        /// <summary> Get one Registry having the id </summary>
        public Registry GetRegistryById(Guid id)
        {
            return this._context.Registries.Where(u => u.Id == id).FirstOrDefault();
        }

        /// <summary> Get all having the Registry </summary>
        //public ICollection<Registry> GetRegistriesByName(string name)
        //{
        //    return _context.Registries.Where(r => r.Name.Contains(name)).ToList();
        //}

        /// <summary> check if id exist </summary>
        public bool RegistryExists(Guid id)
        {
            return this._context.Registries.Any(u => u.Id == id);
        }

        public bool RegistryExists(string registryName)
        {
            return this._context.Registries.Any(u => u.Name.Trim().ToLower() == registryName.Trim().ToLower());
        }
        public bool CreateRegistry(Registry Registry)
        {
            _context.Add(Registry);
            return Save();
        }

        //save the changes on db
        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteRegistry(Registry Registry)
        {
            _context.Remove(Registry);
            return Save();
        }

        public bool UpdateRegistry(Registry Registry)
        {
            _context.Update(Registry);
            return Save();
        }

        public ICollection<Registry> GetRegistries(string name)
        {
            return _context.Registries.Where(r => r.Name.Contains(name)).OrderBy(u => u.Id).ToList();
        }

        public IDictionary<string, List<Registry>> GetClassroom(string classroom)
        {
            var studentRegistries = _context.Registries.Where(r => r.Student.Classroom.Trim().ToLower().Contains(classroom)).ToList();
            var teacherRegistries = _context.Registries.Where(r => r.Teacher.TeacherSubjects.Any(ts => ts.Classroom.Trim().ToLower().Contains(classroom))).ToList();
            ///<summary>concats the two results in an unique List </summary>
            IDictionary<string, List<Registry>> result = new Dictionary<string, List<Registry>>
            { 
                { "students", studentRegistries },
                { "teachers", teacherRegistries }
            };
            return result;
        }
        #endregion

    }
}
