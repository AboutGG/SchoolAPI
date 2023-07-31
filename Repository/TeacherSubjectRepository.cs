using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Repository
{
    public class TeacherSubjectRepository : ITeacherSubjectRepository
    {
        #region Attributes
        private readonly SchoolContext _context;
        #endregion

        #region Costructor
        public TeacherSubjectRepository(SchoolContext context)
        {
            this._context = context;
        }
        #endregion

        #region Interfaces
        public ICollection<TeacherSubject> GetTeachersSubjects()
        {
            return this._context.TeacherSubjects.OrderBy(ts => ts.TeacherId).ToList();
        }

        public bool CreateTeacherSubject(TeacherSubject teacherSubject)
        {
            this._context.Add(teacherSubject);
            return Save();
        }

        public bool Save()
        {
            return this._context.SaveChanges() > 0 ? true : false;
        }
        #endregion
    }
}
