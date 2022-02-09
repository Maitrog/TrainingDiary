using Microsoft.EntityFrameworkCore;

namespace TrainingDiary.Data.Repositories
{
    internal static class UserRepository
    {
        internal async static Task<List<User>> GetUserAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Users.ToListAsync();
            }
        }

        internal async static Task<User> GetUserByIdAsync(string id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        internal async static Task<bool> CreateUserAsync(User user)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Users.AddAsync(user);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateUserAsync(User user)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Users.Update(user);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DaleteUserByIdAsync(string id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    User userToDelete = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
                    db.Users.Remove(userToDelete);

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
