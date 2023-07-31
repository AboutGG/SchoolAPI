namespace SchoolAPI.DTO
{
    public class EntityDTO
    {
        public UserDTO User { get; set; }

        public RegistryDTO Registry { get; set; }

        public bool isStudent { get; set; }

        public string? Classroom { get; set; }
    }
}
