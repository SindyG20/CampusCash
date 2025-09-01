var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS (use default policy)
app.UseCors();

// ----------------- REGISTER ENDPOINT -----------------
app.MapPost("/api/auth/register", (UserRegistration user) =>
{
    // In a real app: save user to database here
    Console.WriteLine($"✅ New user registered: {user.StudentId}, {user.FullName}");

    return Results.Ok(new { message = "User registered successfully!" });
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
