using FluentAssertions;
using JJBanking.Domain.Entities;
using Xunit;

namespace JJBanking.UnitTests.Entities;

public class AccountTests
{
    [Fact]
    public void Constructor_WhenValidData_ShouldCreateAccountWithInitialBalance()
    {
        // arrange (organizar)
        var owner = "Jamerson";
        var cpf = "09478200000";
        var initialDeposit = 700.00m;

        // Act (agir)
        var account = new Account(owner, cpf, initialDeposit);

        // Assert (validar) usando FluentAssertions
        account.Balance.Should().Be(700.00m);
        account.Owner.Should().Be(owner);
        account.Cpf.Should().Be(cpf);
        account.Id.Should().NotBeEmpty(); // Garante que o GUID foi gerado
    }

    [Fact]
    public void Deposit_WhenValueIsPositive_ShouldIncreaseBalance()
    {
        // Arrange
        var account = new Account("Jamerson", "12345678901", 100.00m);

        // Act
        account.Deposit(50.00m);

        // Assert
        account.Balance.Should().Be(150.00m);
    }

    [Fact]
    public void Constructor_WhenInitialDepositIsNegative_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => new Account("Jamerson", "12345678901", -10.00m);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("O depósito inicial não pode ser negativo. (Parameter 'initialDeposit')");
    }
}
