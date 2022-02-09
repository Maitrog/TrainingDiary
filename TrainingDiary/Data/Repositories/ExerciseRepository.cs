using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data.Repositories
{
    internal static class ExerciseRepository
    {
        internal async static Task<List<Exercise>> GetExerciseAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Exercises.ToListAsync();
            }
        }

        internal async static Task<Exercise> GetExerciseByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        internal async static Task<bool> CreateExerciseAsync(Exercise exercise)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Exercises.AddAsync(exercise);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateExerciseAsync(Exercise exercise)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Exercises.Update(exercise);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DaleteExerciseByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Exercise exerciseToDelete = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
                    db.Exercises.Remove(exerciseToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
