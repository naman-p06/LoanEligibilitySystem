using Microsoft.EntityFrameworkCore;
using LoanEligibilitySystem.Data;
using LoanEligibilitySystem.Repositories;
using LoanEligibilitySystem.Services;
using LoanEligibilitySystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanEligibilityService, LoanEligibilityService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");   // ← must be before UseAuthorization
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();