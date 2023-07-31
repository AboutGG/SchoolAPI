using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SchoolAPI.Utils;

namespace SchoolAPI.Models
{
    [Table("registries")]
    public class Registry
    {
        [Column("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [JsonPropertyName("name")]
        [StringValidator(3, ErrorMessage = "name cannot contain less then 3 character")]
        public string Name { get; set; }

        [Column("surname")]
        [JsonPropertyName("surname")]
        [StringValidator(3,ErrorMessage = "surname cannot contain less then 3 character")]
        public string Surname { get; set; }

        [Column("birth")]
        [JsonPropertyName("birth")]
        [DateValidator(ErrorMessage ="Date must be a previous date")]
        public DateOnly Birth { get; set; }

        #region References from another table
        /// <summary> Elements from another table </summary>
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
        public virtual IList<RegistryExam> RegistryExams { get; set; }
        #endregion
    }
}