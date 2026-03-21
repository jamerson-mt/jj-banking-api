using FluentAssertions;
using JJBanking.Domain.Entities;
using Xunit;

namespace JJBanking.UnitTests.Entities;

public class AccountTests
{
    private string GenerateRandomCpf() =>
        Random.Shared.Next(100000000, 999999999).ToString() + "00"; //

    [Fact]
    public void Constructor_WhenValidData_ShouldCreateAccountWithInitialBalance()
    {
        // arrange (organizar)
        var owner = "Jamerson";
        var cpf = GenerateRandomCpf();
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
        var account = new Account("Jamerson", GenerateRandomCpf(), 100.00m);

        // Act
        account.Deposit(50.00m);

        // Assert
        account.Balance.Should().Be(150.00m);
    }

    [Fact]
    public void Constructor_WhenInitialDepositIsNegative_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => new Account("Jamerson", GenerateRandomCpf(), -10.00m);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("O depósito inicial não pode ser negativo. (Parameter 'initialDeposit')");
    }

    [Fact]
    public void Withdraw_WhenBalanceIsSufficient_ShouldDecreaseBalance()
    {
        // Arrange (criamos a conta com saldo)
        var account = new Account("Jamerson", GenerateRandomCpf(), 400.00m);

        // Act (sacamos)
        account.Withdraw(100.00m);

        // Assert ()
        account.Balance.Should().Be(300);
    }

    [Fact]
    public void Withdraw_WhenBalanceIsInsufficient_ShouldThrowException()
    {
        // Arrange (criamos a conta com saldo)
        var account = new Account("Jamerson", GenerateRandomCpf(), 400.00m);

        // Act (sacamos)
        Action act = () => account.Withdraw(500.00m);

        // Assert ()
        act.Should().Throw<InvalidOperationException>().WithMessage("Saldo insuficiente.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Withdraw_WhenValueIsInvalid_ShouldThrowException(decimal invalidAmount)
    {
        // Arrange
        var account = new Account("Jamerson", GenerateRandomCpf(), 300);

        //Act
        Action act = () => account.Withdraw(invalidAmount);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("O valor do saque deve ser positivo.");
    }
}
