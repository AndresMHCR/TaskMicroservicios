using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using PersonaService.Models;
using PersonaService.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersonaService.Models.Dto;

namespace MyApp.IntegrationTests
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly PersonaContext _personaContext;

        public IntegrationTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Reemplaza el DbContext por uno en memoria para pruebas
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PersonaContext>));
                    services.Remove(descriptor);
                    services.AddDbContext<PersonaContext>(options =>
                    {
                        options.UseSqlServer("Server=andresPC\\SQLEXPRESS;Database=PersonaTest;Trusted_Connection=true;User ID=sa;Password=SqlS3rv3r999;MultipleActiveResultSets=true");
                    });
                });
            }).CreateClient();

            // Inicializa el contexto de la base de datos
            using var scope = factory.Services.CreateScope();
            _personaContext = scope.ServiceProvider.GetRequiredService<PersonaContext>();
            _personaContext.Database.EnsureCreated(); // Crea la base de datos para pruebas
        }

        [Fact]
        public async Task CreateClient_ValidClient_ReturnsCreatedAtRoute()
        {
            // Arrange
            var cliente = new Cliente
            {
                Idcliente = 0,
                Idpersona = 2,
                Clienteid = "JuaNOS088",
                Contrasena = "P@ssw0rdJuan",
                Estadocliente = true
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/clientes", cliente);

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica 200

            var createdResult = Assert.IsType<Cliente>(JsonConvert.DeserializeObject <Cliente> (await response.Content.ReadAsStringAsync()));
            Assert.Equal(2, createdResult.Idpersona);
            Assert.NotEqual(cliente.Idcliente, createdResult.Idcliente);
        }

    }
}