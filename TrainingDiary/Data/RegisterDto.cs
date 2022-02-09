using System.ComponentModel.DataAnnotations;

namespace TrainingDiary.Data
{
    public class RegisterDto
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime Birthday { get; set; }

        public string Country { get; set; } = string.Empty;
    }
}
