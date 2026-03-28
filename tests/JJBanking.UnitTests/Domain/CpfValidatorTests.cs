using FluentAssertions;
using JJBanking.API.Utils;

namespace JJBanking.UnitTests.Domain;

public class CpfValidatorTests
{
    // TESTES BASEADOS EM CASOS COMUNS DE CPF INVÁLIDO E VÁLIDO
    [Theory]
    [InlineData("11111111111")] // Repetidos
    [InlineData("12345678901")] // Cálculo inválido
    public void ValidarCpf_DeveRetornarFalso_ParaCpfsInvalidos(string cpf)
    {
        var resultado = CpfValidator.IsValidCpf(cpf);
        resultado.Should().BeFalse();
    }

    // TESTE PARA UM CPF VÁLIDO
    [Fact]
    public void ValidarCpf_DeveRetornarTrue_ParaCpfValido()
    {
        var resultado = CpfValidator.IsValidCpf("52998224725"); // Use um real válido
        resultado.Should().BeTrue();
    }
}
