using SchoolAPI.Utils;

namespace SchoolAPI.DTO
{
    public class RegistryDTO
    {
        /// <summary> User id </summary>
        public Guid Id { get; set; }

        /// <summary> name </summary>
        [StringValidator(3, ErrorMessage ="Name cannot contain less then 3 character")]
        public string Name { get; set; }

        /// <summary> surname </summary>
        [StringValidator(3, ErrorMessage = "Surname cannot contain less then 3 character")]
        public string Surname { get; set; }

        /// <summary> surname </summary>
        [DateValidator(ErrorMessage ="Birth must be previous date")]
        [AgeRange(13, 70, ErrorMessage = "The user’s age must be between 13 and 70 years.")]
        public DateOnly Birth { get; set; }

    }
}