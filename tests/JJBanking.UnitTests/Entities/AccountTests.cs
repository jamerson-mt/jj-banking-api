using FluentAssertions;
using JJBanking.Domain.Entities;
using Xunit;

namespace JJBanking.UnitTests.Entities;

public class AccountTests
{
    private string GenerateRandomCpf() =>
        Random.Shared.Next(100000000, 999999999).ToString() + "00"; //

    // [Fact]
    // public void Deposit_WhenValueIsPositive_ShouldIncreaseBalance() { }

    // [Fact]
    // public void Withdraw_WhenBalanceIsSufficient_ShouldDecreaseBalance() //
    // { }

    // [Fact]
    // public void Withdraw_WhenBalanceIsInsufficient_ShouldThrowException() { }

    // [Theory]
    // [InlineData(0)]
    // [InlineData(-50)]
    // public void Withdraw_WhenValueIsInvalid_ShouldThrowException(decimal invalidAmount) { }
}
