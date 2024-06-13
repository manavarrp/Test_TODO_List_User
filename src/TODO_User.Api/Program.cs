using TODO_User.Api.Extensions;
using TODO_User.Application;
using TODO_User.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Cors = "Cors";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configurar la base de datos
builder.Services.ConfigureDatabase(builder.Configuration);
// Configurar la autenticación y autorización
builder.Services.ConfigureIdentity();
builder.Services.AddDataProtection();
// Configurar servicios de la aplicación
builder.Services.AddAplicationServices();

//Add authentication to Swagger UI
builder.Services.ConfigureSwagger();

// Configurar CORS para permitir cualquier origen, método y cabecera
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(Cors);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Habilitar la autorización - autenticación
app.UseAuthentication();
app.UseAuthorization();
// Aplicar migraciones de base de datos
app.MigrateDatabase();
app.MapControllers();

app.Run();
