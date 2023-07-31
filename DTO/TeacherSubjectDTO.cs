
using SchoolAPI.Utils;

namespace SchoolAPI.DTO
{
    public class TeacherSubjectDTO
    {
        public Guid TeacherId { get; set; }

        public Guid SubjectId { get; set; }

        [StringValidator(2)]
        public string Classroom { get; set; }
    }
}