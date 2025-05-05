using System.ComponentModel.DataAnnotations;

namespace Demo_1670.Models
{
    public class Employer
    {
        [Key]
        public string Username { get; set; }

        public string CompanyName { get; set; }
        public string Email { get; set; } 
        public string Description { get; set; }
        public string Industry { get; set; }
    }
}
