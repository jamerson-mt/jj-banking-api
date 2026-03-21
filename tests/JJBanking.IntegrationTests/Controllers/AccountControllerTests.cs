using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JJBanking.API.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace JJBanking.IntegrationTests.Controllers;

// Program é a classe principal da sua API
public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private string GenerateRandomCpf() =>
        Random.Shared.Next(100000000, 999999999).ToString() + "00";

    private readonly HttpClient _client;

    public AccountControllerTests(WebApplicationFactory<Program> factory)
    {
        // Cria um "cliente" que sabe conversar com a sua API em memória
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAccount_WhenDataIsValid_ShouldReturnCreated()
    {
        // Arrange
        var request = new
        {
            Owner = "Jamerson Teste",
            Cpf = GenerateRandomCpf(), // O CPF DEVE SER UNICO PARA CADA TESTE, POIS A API NAO PERMITE DUPLICADOS
            InitialDeposit = 100.00m,
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/accounts", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Lendo o JSON usando o molde AccountResponse
        var content = await response.Content.ReadFromJsonAsync<AccountResponse>();

        content.Should().NotBeNull();
        content!.Id.Should().NotBeEmpty();
        content.Owner.Should().Be(request.Owner);
    }
}
