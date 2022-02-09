using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingDiary.Data
{
    public sealed class Workout
    {
        [Key]
        public int Id { get; set; }
        public List<Set> Sets { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Range(0, 1000000)]
        public int Duration { get; set; }

        [Range(0, 10000)]
        public int Calories { get; set; }

        [Range(0, 300)]
        public int MaxPulse { get; set; }

        [Range(0, 300)]
        public int AvgPulse { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
