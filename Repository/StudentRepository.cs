using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {

        #region Attributes
        private readonly SchoolContext _context;
        #endregion

        #region Costructor
        public StudentRepository(SchoolContext context)
        {
            this._context = context;
        }
        #endregion

        public bool CreateStudent(Student student)
        {
            _context.Add(student);
            return Save();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.OrderBy(u => u.Id).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
