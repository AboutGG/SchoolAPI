﻿using SchoolAPI.Utils;

namespace SchoolAPI.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        [StringValidator(3, ErrorMessage = "Username cannot contain less then 3 character")]
        public string Classroom { get; set; }
        public Guid UserId { get; set; }

        public Guid RegistryId { get; set; }
    }
}
