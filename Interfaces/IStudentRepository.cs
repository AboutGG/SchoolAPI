using SchoolAPI.Models;

namespace SchoolAPI.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        bool CreateStudent(Student student);
        bool Save();
    }
}
