using SchoolAPI.Models;
using SchoolAPI.Interfaces;

namespace SchoolAPI.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        #region Attributes
        private readonly SchoolContext _context;
        #endregion

        #region Costructor
        public TeacherRepository(SchoolContext context)
        {
            this._context = context;
        }
        #endregion

        #region Interfaces
        public bool CreateTeacher(Teacher teacher)
        {
            this._context.Add(teacher);
            return Save();
        }

        public ICollection<Teacher> GetTeachers()
        {
            return this._context.Teachers.OrderBy(u => u.Id).ToList();
        }

        public bool Save()
        {
            return this._context.SaveChanges() > 0 ? true : false;
        }

        public bool TeacherExists(Guid teacherId)
        {
            return this._context.Teachers.Any(t => t.Id == teacherId);
        }
        #endregion
    }
}
