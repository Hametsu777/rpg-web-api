using Microsoft.EntityFrameworkCore;
using RPGWebAPI.Data;
using RPGWebAPI.Services.CharacterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Need to study more on automapper.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Add this so the api knows to use the character service class when a controller wants to inject the ICharacter service.
// This basically registers the ICharacterService. AddScoped creates a new instance of the requested service for every request
// that comes in. AddTransient provides a new instance to every controller and to every service even within the same request.
// AddSingleton creates only one instance that is used for every request.
builder.Services.AddScoped<ICharacterService, CharacterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
