
namespace SchoolAPI.DTO
{
    public class TeacherDTO
    {
        public Guid Id { get; set; }

        /// <summary> Teacher registry id </summary>
        public Guid RegistryId { get; set; }

        /// <summary> Teacher user id </summary>
        public Guid UserId { get; set; }
    }
}
