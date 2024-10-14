using Moq;
using Microsoft.AspNetCore.Mvc;
using PersonaService.Controllers; 
using PersonaService.Models;
using PersonaService.Repository;
using PersonaService.Repository.Interface;
using Microsoft.AspNetCore.Http;
public class ClienteControllerTests
{
    private readonly Mock<IPersonaRepository> _mockRepository;
    private readonly ClientesController _controller;
    public ClienteControllerTests()
    {
        _mockRepository = new Mock<IPersonaRepository>();
        _controller = new ClientesController(_mockRepository.Object);
    }

    

    [Fact]
    public async Task CreateClient_ValidClient_ReturnsClient()
    {
        // Arrange
        var cliente = new Cliente
        {
            Idcliente = 0,
            Idpersona = 2,
            Clienteid = "MarMon007",
            Contrasena = "P@ssw0rd",
            Estadocliente = true
        };

        _mockRepository.Setup(repo => repo.GetClient(cliente.Idcliente))
            .ReturnsAsync((Cliente)null); 
        _mockRepository.Setup(repo => repo.CreateClient(cliente))
            .Returns(Task.CompletedTask); 

        var result = await _controller.CreateClient(cliente);

        // Verifica Resultados Pruebas
        var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
        Assert.Equal("GetCliente", createdResult.RouteName);
        Assert.Equal(cliente.Idcliente, createdResult.RouteValues["idClient"]);
    }

    [Fact]
    public async Task CreateClient_ClientExists_ReturnsBadRequest()
    {
        // Arrange
        var clienteExistente = new Cliente
        {
            Idcliente = 1,
            Idpersona = 2,
            Clienteid = "MarMon007",
            Contrasena = "P@ssw0rd",
            Estadocliente = true
        };

        _mockRepository.Setup(repo => repo.GetClient(clienteExistente.Idcliente))
            .ReturnsAsync(clienteExistente);

        // Act
        var result = await _controller.CreateClient(clienteExistente);

        // Verifica Resultados Pruebas
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains(badRequestResult.StatusCode.ToString(), "400");
    }
}
