using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var one=builder.Configuration["ConnectionStrings:ApplicationDbContext"];
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

builder.Services.AddScoped<INationRepostory, NationRepository>();
builder.Services.AddScoped<IGameSessionRepository, GameSessionRepository>();

builder.Services.AddScoped<INationService, NationService>();
builder.Services.AddScoped<IGameSessionService, GameSessionService>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        //.WithOrigins("http://test.ge:4200")
        //.AllowAnyOrigin()
        //.AllowAnyMethod()
        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
        .AllowAnyHeader()
        .SetIsOriginAllowed(hostName => true)
        .AllowCredentials();
    });
});


var app = builder.Build();

//seed data

app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<ApplicationDbContext>();
        await SeedData.SeedAsync(catalogContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
//end seed data

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
