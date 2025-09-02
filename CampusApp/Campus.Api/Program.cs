var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

// ----------------- In-memory storage -----------------
List<UserRegistration> users = new List<UserRegistration>();

// ----------------- REGISTER ENDPOINT -----------------
app.MapPost("/api/auth/register", (UserRegistration user) =>
{
    if (users.Any(u => u.StudentId == user.StudentId))
        return Results.BadRequest(new { message = "StudentID already registered." });

    if (users.Any(u => u.StudentNumber == user.StudentNumber))
        return Results.BadRequest(new { message = "Student Number already registered." });

    users.Add(user);
    Console.WriteLine($"✅ New user registered: {user.StudentId}, {user.FullName}");
    return Results.Ok(new { message = "User registered successfully!" });
});

// ----------------- LOGIN ENDPOINT -----------------
app.MapPost("/api/auth/login", (LoginRequest request) =>
{
    var existingUser = users.FirstOrDefault(u => u.StudentNumber == request.StudentNumber);

    if (existingUser == null)
        return Results.BadRequest(new { message = "Student number not found." });

    if (existingUser.PasswordHash != request.Password)
        return Results.BadRequest(new { message = "Incorrect password." });

    return Results.Ok(new { message = "Login successful!", fullName = existingUser.FullName });
});

app.Run();

// ----------------- MODELS -----------------
public class UserRegistration
{
    public string StudentId { get; set; } = "";
    public string StudentNumber { get; set; } = "";
    public string FullName { get; set; } = "";
    public string DateOfBirth { get; set; } = "";
    public string Address { get; set; } = "";
    public string AccountType { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
}

public class LoginRequest
{
    public string StudentNumber { get; set; } = "";
    public string Password { get; set; } = "";
}
