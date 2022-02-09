using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TrainingDiary.Data
{
    public class User: IdentityUser
    {
        [Required]
        public DateTime Birthday { get; set; }
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        public List<Workout> Workouts { get; set; }
    }
}
