using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Repository
{
    public class ExamRepository : IExamRepository
    {
        #region Attributes
        private readonly SchoolContext _context;
        #endregion

        #region Costructor
        public ExamRepository(SchoolContext context)
        {
            this._context = context;
        }
        #endregion
        
        public bool CreateExam(Exam exam)
        {
            this._context.Add(exam);
            return Save();
        }

        public ICollection<Exam> GetExams()
        {
            return this._context.Exams.OrderBy(u => u.Id).ToList();
        }

        public bool Save()
        {
            return this._context.SaveChanges() > 0 ? true : false;
        }
    }

}

