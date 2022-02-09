using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data.Repositories
{
    internal static class MuscleGroupRepository
    {
        internal async static Task<List<MuscleGroup>> GetMuscleGroupAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.MuscleGroups.ToListAsync();
            }
        }

        internal async static Task<MuscleGroup> GetMuscleGroupByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.MuscleGroups.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        internal async static Task<bool> CreateMuscleGroupAsync(MuscleGroup muscleGroup)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.MuscleGroups.AddAsync(muscleGroup);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateMuscleGroupAsync(MuscleGroup muscleGroup)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.MuscleGroups.Update(muscleGroup);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DaleteMuscleGroupByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    MuscleGroup muscleGroupToDelete = await db.MuscleGroups.FirstOrDefaultAsync(x => x.Id == id);
                    db.MuscleGroups.Remove(muscleGroupToDelete);

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
