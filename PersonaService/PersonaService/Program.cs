
using Microsoft.EntityFrameworkCore;
using PersonaService.Repository;
using PersonaService.Repository.Implementation;
using PersonaService.Repository.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<PersonaContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
        });

        builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

        builder.Services.AddControllers(option =>
        {
            option.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
    }
}
