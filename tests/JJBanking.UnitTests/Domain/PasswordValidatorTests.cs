using FluentAssertions;
using JJBanking.API.Utils;
using Xunit;

namespace JJBanking.UnitTests.Domain;

public class PasswordValidatorTests
{
    // TESTES PARA VALIDAR A FORÇA DA SENHA USADA NO REGISTRO DE USUÁRIO, GARANTINDO QUE AS REGRAS DE SEGURANÇA SEJAM CUMPRIDAS
    [Theory]
    [InlineData("1234567")] // Curta demais
    [InlineData("senhateste123")] // Sem maiúscula
    [InlineData("SENHATESTE123")] // Sem minúscula
    [InlineData("SenhaTeste")] // Sem número
    [InlineData("Senha123")] // Sem caractere especial
    [InlineData("")] // Vazia
    public void IsStrong_DeveRetornarFalso_QuandoSenhaNaoCumpreRequisitos(string senhaFraca)
    {
        // Act
        var resultado = PasswordValidator.IsStrong(senhaFraca);

        // Assert
        resultado.Should().BeFalse($"a senha '{senhaFraca}' é ser considerada fraca.");
    }

    // TESTE PARA VER SE A SENHA FORTE É RECONHECIDA CORRETAMENTE, GARANTINDO QUE O MÉTODO DE VALIDAÇÃO FUNCIONE COMO ESPERADO
    [Fact]
    public void IsStrong_DeveRetornarVerdadeiro_QuandoSenhaForForte()
    {
        // Arrange
        var senhaForte = "Jamerson@2026";

        // Act
        var resultado = PasswordValidator.IsStrong(senhaForte);

        // Assert
        resultado.Should().BeTrue(); // A senha 'Jamerson@2026' atende a todos os critérios de força, portanto deve ser considerada forte
    }
}
