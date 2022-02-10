using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TrainingDiary.Data;
using Microsoft.AspNetCore.Mvc;
using TrainingDiary.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
    });
});

builder.Services.AddDbContext<AppDBContext>();
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!¹;%:?*()-=/#$^&[]{}\\|<>,:'\"";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Training Diary API",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUIOptions =>
    {
        swaggerUIOptions.DocumentTitle = "Training Diary API";
        swaggerUIOptions.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
        swaggerUIOptions.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("login", [ValidateAntiForgeryToken] async (LoginDto userDto) =>
{
    User? user;
    using (var db = new AppDBContext())
    {
         user = await db.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
    }
    if (user != null)
    {
        using (var scope = app.Services.CreateScope())
        {

            var signInManager = (SignInManager<User>)scope.ServiceProvider.GetService(typeof(SignInManager<User>));
            var result = await signInManager.PasswordSignInAsync(user, userDto.Password, userDto.RememberMe, false);
            if (result.Succeeded)
            {
                return Results.Ok("Successfully login.");
            }
            else
            {
                return Results.BadRequest("Error. Invalid Login or Password.");
            }
        }
    }
    return Results.BadRequest("Error. Invalid Login.");
}).WithTags("Authorization Endpoints");

app.MapPost("logout", [ValidateAntiForgeryToken][Authorize] async () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var signInManager = (SignInManager<User>)scope.ServiceProvider.GetService(typeof(SignInManager<User>));
        await signInManager.SignOutAsync();
        return Results.Ok("Successfully logout.");
    }
}).WithTags("Authorization Endpoints");

app.MapPost("register", async (RegisterDto registerDto) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
        var signInManager = (SignInManager<User>)scope.ServiceProvider.GetService(typeof(SignInManager<User>));

        User user = new()
        {
            Email = registerDto.Email,
            Birthday = registerDto.Birthday,
            Country = registerDto.Country,
            PhoneNumber = registerDto.PhoneNumber,
            UserName = registerDto.Login
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await userManager.ConfirmEmailAsync(user, code);
            await userManager.AddToRoleAsync(user, "user");
            await signInManager.SignInAsync(user, false);
            return Results.Ok("Successfuly register.");
        }
        return Results.BadRequest("Error. Register failed.");
    }
}).WithTags("Authorization Endpoints");

app.MapGet("get-all-exercise-type", [Authorize(Roles = "admin,moderator")] async () => await ExerciseTypeRepository.GetExerciseTypeAsync()).WithTags("Exercise Types Endpoints");

app.MapGet("/get-exercise-type-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    ExerciseType exerciseType = await ExerciseTypeRepository.GetExerciseTypeByIdAsync(id);
    if (exerciseType != null)
    {
        return Results.Ok(exerciseType);
    }
    return Results.BadRequest();
}).WithTags("Exercise Types Endpoints");

app.MapPut("/update-exercise-type", [Authorize(Roles = "admin,moderator")] async (ExerciseType exerciseType) =>
{
    bool result = await ExerciseTypeRepository.UpdateExerciseTypeAsync(exerciseType);
    if (result)
    {
        return Results.Ok();
    }
    return Results.BadRequest("Update successful");
}).WithTags("Exercise Types Endpoints");

app.MapPost("/create-exercise-type", [Authorize(Roles = "admin,moderator")] async (ExerciseType exerciseType) =>
{
    bool result = await ExerciseTypeRepository.CreateExerciseTypeAsync(exerciseType);
    if (result)
    {
        return Results.Ok("Create successful");
    }
    return Results.BadRequest();
}).WithTags("Exercise Types Endpoints");

app.MapDelete("/delete-exercise-type-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    bool result = await ExerciseTypeRepository.DaleteExerciseTypeByIdAsync(id);
    if (result)
    {
        return Results.Ok("Delete successful");
    }
    return Results.BadRequest();
}).WithTags("Exercise Types Endpoints");

app.MapGet("/get-all-muscle-group", [Authorize] async () => await MuscleGroupRepository.GetMuscleGroupAsync()).WithTags("Muscle Groups Endpoints");

app.MapGet("/get-muscle-group-by-id/{id}", [Authorize] async (int id) =>
{
    MuscleGroup muscleGroup = await MuscleGroupRepository.GetMuscleGroupByIdAsync(id);
    if (muscleGroup != null)
    {
        return Results.Ok(muscleGroup);
    }
    return Results.BadRequest();
}).WithTags("Muscle Groups Endpoints");

app.MapPut("/update-muscle-group", [Authorize(Roles = "admin,moderator")] async (MuscleGroup muscleGroup) =>
{
    bool result = await MuscleGroupRepository.UpdateMuscleGroupAsync(muscleGroup);
    if (result)
    {
        return Results.Ok();
    }
    return Results.BadRequest("Update successful");
}).WithTags("Muscle Groups Endpoints");

app.MapPost("/create-muscle-group", [Authorize(Roles = "admin,moderator")] async (MuscleGroup muscleGroup) =>
{
    bool result = await MuscleGroupRepository.CreateMuscleGroupAsync(muscleGroup);
    if (result)
    {
        return Results.Ok("Create successful");
    }
    return Results.BadRequest();
}).WithTags("Muscle Groups Endpoints");

app.MapDelete("/delete-muscle-group-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    bool result = await MuscleGroupRepository.DaleteMuscleGroupByIdAsync(id);
    if (result)
    {
        return Results.Ok("Delete successful");
    }
    return Results.BadRequest();
}).WithTags("Muscle Groups Endpoints");

app.MapGet("/get-all-exercise", [Authorize] async () => await ExerciseRepository.GetExerciseAsync()).WithTags("Exercises Endpoints");

app.MapGet("/get-exercise-by-id/{id}", [Authorize] async (int id) =>
{
    Exercise exercise = await ExerciseRepository.GetExerciseByIdAsync(id);
    if (exercise != null)
    {
        return Results.Ok(exercise);
    }
    return Results.BadRequest();
}).WithTags("Exercises Endpoints");

app.MapPut("/update-exercise", [Authorize(Roles = "admin,moderator")] async (Exercise exercise) =>
{
    bool result = await ExerciseRepository.UpdateExerciseAsync(exercise);
    if (result)
    {
        return Results.Ok();
    }
    return Results.BadRequest("Update successful");
}).WithTags("Exercises Endpoints");

app.MapPost("/create-exercise", [Authorize] async (Exercise exercise) =>
{
    bool result = await ExerciseRepository.CreateExerciseAsync(exercise);
    if (result)
    {
        return Results.Ok("Create successful");
    }
    return Results.BadRequest();
}).WithTags("Exercises Endpoints");

app.MapDelete("/delete-exercise-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    bool result = await ExerciseRepository.DaleteExerciseByIdAsync(id);
    if (result)
    {
        return Results.Ok("Delete successful");
    }
    return Results.BadRequest();
}).WithTags("Exercises Endpoints");

app.MapGet("/get-all-set", [Authorize(Roles = "admin,moderator")] async () => await SetRepository.GetSetAsync()).WithTags("Sets Endpoints");

app.MapGet("/get-set-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    Set set = await SetRepository.GetSetByIdAsync(id);
    if (set != null)
    {
        return Results.Ok(set);
    }
    return Results.BadRequest();
}).WithTags("Sets Endpoints");

app.MapPut("/update-set", [Authorize] async (Set set) =>
{
    bool result = await SetRepository.UpdateSetAsync(set);
    if (result)
    {
        return Results.Ok();
    }
    return Results.BadRequest("Update successful");
}).WithTags("Sets Endpoints");

app.MapPost("/create-set", [Authorize] async (Set set) =>
{
    bool result = await SetRepository.CreateSetAsync(set);
    if (result)
    {
        return Results.Ok("Create successful");
    }
    return Results.BadRequest();
}).WithTags("Sets Endpoints");

app.MapDelete("/delete-set-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    bool result = await SetRepository.DaleteSetByIdAsync(id);
    if (result)
    {
        return Results.Ok("Delete successful");
    }
    return Results.BadRequest();
}).WithTags("Sets Endpoints");

app.MapGet("/get-all-workout", [Authorize] async () => await WorkoutRepository.GetWorkoutAsync()).WithTags("Workouts Endpoints");

app.MapGet("/get-workout-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    Workout workout = await WorkoutRepository.GetWorkoutByIdAsync(id);
    User? user;
    using (var scope = app.Services.CreateScope())
    {
        var httpContextAccessor = (IHttpContextAccessor)scope.ServiceProvider.GetService(typeof(IHttpContextAccessor));
        var userClaim = httpContextAccessor?.HttpContext?.User;
        var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
        user = await userManager.GetUserAsync(userClaim);
    }
    //TODO: Add role check (ADMIN and MODERATOR)
    if(user.Id != workout.UserId)
    {
        return Results.BadRequest();
    }
    if (workout != null)
    {
        return Results.Ok(workout);
    }
    return Results.BadRequest();
}).WithTags("Workouts Endpoints");

app.MapPut("/update-workout", [Authorize] async (Workout workout) =>
{
    bool result = await WorkoutRepository.UpdateWorkoutAsync(workout);
    if (result)
    {
        return Results.Ok();
    }
    return Results.BadRequest("Update successful");
}).WithTags("Workouts Endpoints");

app.MapPost("/create-workout", [Authorize] async (Workout workout) =>
{
    bool result = await WorkoutRepository.CreateWorkoutAsync(workout);
    if (result)
    {
        return Results.Ok("Create successful");
    }
    return Results.BadRequest();
}).WithTags("Workouts Endpoints");

app.MapDelete("/delete-workout-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (int id) =>
{
    bool result = await WorkoutRepository.DaleteWorkoutByIdAsync(id);
    if (result)
    {
        return Results.Ok("Delete successful");
    }
    return Results.BadRequest();
}).WithTags("Workouts Endpoints");

app.MapGet("/get-all-user", [Authorize(Roles = "admin,moderator")] async () => await UserRepository.GetUserAsync()).WithTags("Users Endpoints");

app.MapGet("/get-user-by-id/{id}", [Authorize(Roles = "admin,moderator")] async (string id) =>
{
    User user = await UserRepository.GetUserByIdAsync(id);
    if (user != null)
    {
        return Results.Ok(user);
    }
    return Results.BadRequest();
}).WithTags("Users Endpoints");

app.MapPut("/update-user", [Authorize(Roles = "admin")] async (User user) =>
{
    bool result = await UserRepository.UpdateUserAsync(user);
    if (result)
    {
        return Results.Ok();
    }
    return Results.BadRequest("Update successful");
}).WithTags("Users Endpoints");

app.MapPost("/create-user", [Authorize(Roles = "admin")] async (User user) =>
{
    bool result = await UserRepository.CreateUserAsync(user);
    if (result)
    {
        return Results.Ok("Create successful");
    }
    return Results.BadRequest();
}).WithTags("Users Endpoints");

app.MapDelete("/delete-user-by-id/{id}", [Authorize(Roles = "admin")] async (string id) =>
{
    bool result = await UserRepository.DaleteUserByIdAsync(id);
    if (result)
    {
        return Results.Ok("Delete successful");
    }
    return Results.BadRequest();
}).WithTags("Users Endpoints");

app.MapGet("/get-all-roles", /*[Authorize(Roles = "admin,moderator")]*/ async () =>
{
    using (var db = new AppDBContext())
    {
        return await db.Roles.ToListAsync();
    }
}).WithTags("Roles Endpoints");

app.MapGet("/get-all-user-roles", [Authorize] async () =>
{
    using (var db = new AppDBContext())
    {
        return await db.UserRoles.ToListAsync();
    }
}).WithTags("Roles Endpoints");
app.Run();