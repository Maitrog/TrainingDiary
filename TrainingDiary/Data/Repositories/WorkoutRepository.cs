using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data.Repositories
{
    internal static class WorkoutRepository
    {
        internal async static Task<List<Workout>> GetWorkoutAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Workouts.ToListAsync();
            }
        }

        internal async static Task<Workout> GetWorkoutByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Workouts.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
        internal async static Task<List<Workout>> GetWorkoutByUserIdAsync(string userId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Workouts.Where(x => x.UserId == userId).ToListAsync();
            }
        }

        internal async static Task<bool> CreateWorkoutAsync(Workout workout)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Workouts.AddAsync(workout);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateWorkoutAsync(Workout workout)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Workouts.Update(workout);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DaleteWorkoutByIdAsync(int id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Workout workoutToDelete = await db.Workouts.FirstOrDefaultAsync(x => x.Id == id);
                    db.Workouts.Remove(workoutToDelete);

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
