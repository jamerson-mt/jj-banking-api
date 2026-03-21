// crie classe para TransactionControllerTests seguindo o modelo de AccountControllerTests, mas testando os endpoints de depósito e saque da TransactionController
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JJBanking.API.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;

namespace JJBanking.IntegrationTests.Controllers;

public class TransactionControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TransactionControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // TESTE PARA VER SE O ENDPOINT DE DEPÓSITO FUNCIONA CORRETAMENTE
    [Fact]
    public async Task Deposit_WhenDataIsValid_ShouldReturnOk()
    {
        // Arrange
        var accountRequest = new
        {
            Owner = "Jamerson Teste",
            Cpf = GenerateRandomCpf(),
            InitialDeposit = 100.00m,
        };

        var httpResponse = await _client.PostAsJsonAsync("/api/accounts", accountRequest); // Cria uma conta para usar o ID no depósito
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created); // Verifica se a conta foi criada com sucesso

        var accountResponse = await httpResponse.Content.ReadFromJsonAsync<AccountResponse>(); // Lê a resposta para obter o ID da conta criada

        var depositRequest = new DepositRequest(
            accountResponse!.Id, // Usa o ID da conta criada para o depósito. o "!" é para informar que o accountResponse não será nulo, já que a conta foi criada com sucesso
            50.00m,
            "Depósito de teste"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/transaction/deposit", depositRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<TransactionResponse>(); // Lê a resposta do depósito para verificar se os dados estão corretos

        content.Should().NotBeNull(); // não deve ser nulo

        content!.Amount.Should().Be(depositRequest.Amount); // o valor do depósito deve ser igual ao que foi enviado na requisição
        content.Description.Should().Be(depositRequest.Description); // a descrição do depósito deve ser igual à que foi enviada na requisição
    }

    private string GenerateRandomCpf() =>
        Random.Shared.Next(100000000, 999999999).ToString() + "00";
}
