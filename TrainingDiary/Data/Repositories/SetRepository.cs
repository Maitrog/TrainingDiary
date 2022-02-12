using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data.Repositories
{
    internal static class SetRepository
    {
        internal async static Task<List<Set>> GetSetAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Sets.ToListAsync();
            }
        }

        internal async static Task<Set> GetSetByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Sets.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        internal async static Task<List<Set>> GetSetByUserIdAsync(string userId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Sets.Where(x => x.Workout.UserId == userId).ToListAsync();
            }
        }

        internal async static Task<List<Set>> GetSetByWorkoutIdAsync(int workoutId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Sets.Where(x => x.WorkoutId == workoutId).ToListAsync();
            }
        }

        internal async static Task<bool> CreateSetAsync(Set set)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Sets.AddAsync(set);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateSetAsync(Set set)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Sets.Update(set);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DaleteSetByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Set setToDelete = await db.Sets.FirstOrDefaultAsync(x => x.Id == id);
                    db.Sets.Remove(setToDelete);

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
