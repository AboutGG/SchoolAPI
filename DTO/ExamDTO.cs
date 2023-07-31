namespace SchoolAPI.DTO
{
    public class ExamDTO
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        public DateOnly ExamDate { get; set; }
    }
}