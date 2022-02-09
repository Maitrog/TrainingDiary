using System.ComponentModel.DataAnnotations;

namespace TrainingDiary.Data
{
    public sealed class LoginDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
