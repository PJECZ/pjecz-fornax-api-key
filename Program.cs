
// Definir que los nombres de las tablas y columnas de la base de datos usan guiones bajos
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Basic ASP.NET Core Web API application setup
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add endpoints and Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

app.Run();
