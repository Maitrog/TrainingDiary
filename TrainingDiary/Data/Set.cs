using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingDiary.Data
{
    public sealed class Set
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        [Range(0, 10000)]
        public  ushort Quantity { get; set; }

        [Range(0, 1000000)]
        public double Distance { get; set; }

        [Range(0, 1000000)]
        public double Seconds { get; set; }

        [Required]
        public double AdditionalWeight { get; set; } = 0.0;


        [Required]
        public int WorkoutId { get; set; }
        
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; }

    }
}
