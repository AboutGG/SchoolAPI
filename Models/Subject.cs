using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolAPI.Models
{
    [Table("subjects")]
    public class Subject
    {
        /// <summary> subject id </summary>
        [Column("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary> subject name </summary>
        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        #region External ref
        public IList<TeacherSubject> TeacherSubjects { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        #endregion
    }
}
