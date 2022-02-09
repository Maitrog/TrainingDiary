using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data.Repositories
{
    internal static class ExerciseTypeRepository
    {
        internal async static Task<List<ExerciseType>> GetExerciseTypeAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.ExerciseTypes.ToListAsync();
            }
        }

        internal async static Task<ExerciseType> GetExerciseTypeByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.ExerciseTypes.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        internal async static Task<bool> CreateExerciseTypeAsync(ExerciseType exerciseType)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.ExerciseTypes.AddAsync(exerciseType);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateExerciseTypeAsync(ExerciseType exerciseType)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.ExerciseTypes.Update(exerciseType);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DaleteExerciseTypeByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    ExerciseType exerciseTypeToDelete = await db.ExerciseTypes.FirstOrDefaultAsync(x => x.Id == id);
                    db.ExerciseTypes.Remove(exerciseTypeToDelete);

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
