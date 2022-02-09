using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingDiary.Data
{
    public sealed class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int ExerciseTypeId { get; set; }

        [ForeignKey("ExerciseTypeId")]
        public ExerciseType ExerciseType { get; set; }

        [Required]
        [ForeignKey("MuscleGroup")]
        public int MuscleGroupId { get; set; }

        [ForeignKey("MuscleGroupId")]
        public MuscleGroup MuscleGroup { get; set; }
    }

    public sealed class ExerciseType
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
    
    public sealed class MuscleGroup
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
