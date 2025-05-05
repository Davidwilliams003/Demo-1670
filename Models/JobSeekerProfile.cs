using System.ComponentModel.DataAnnotations;

namespace Demo_1670.Models
{
    public class JobSeekerProfile
    {
        [Key] // thêm dòng này
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
    }
}
