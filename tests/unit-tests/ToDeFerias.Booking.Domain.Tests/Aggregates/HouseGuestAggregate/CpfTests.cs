using FluentAssertions;
using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;
using Xunit;

namespace ToDeFerias.Bookings.Domain.Tests.Aggregates.HouseGuestAggregate;

public sealed class CpfTests
{
    [Theory]
    [InlineData("148.550.770-70")]
    [InlineData("06802626012")]
    [InlineData("733054.944-41")]
    [InlineData("016790664-04")]
    [InlineData("339.76734454")]
    public void IsValid_Should_ReturnTrue_When_ValidCpfNumber(string cpf)
    {
        // arrange
        var result = Cpf.IsValid(cpf);

        // assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = @"DADO número de CPF inválido
                            QUANDO executar o método IsValid
                            ENTÃO deve retornar falso")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123413123")]
    [InlineData("44492437810")]
    [InlineData("877941257871")]
    [InlineData("32130312319")]
    public void IsValid_Should_ReturnFalse_When_InvalidCpfNumber(string cpf)
    {
        // arrange
        var result = Cpf.IsValid(cpf);

        // assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123413123")]
    [InlineData("44492437810")]
    [InlineData("877941257871")]
    [InlineData("32130312319")]
    public void NewCpf_Should_ReturnException_When_InvalidCpfNumber(string cpf)
    {
        // act
        var exception = Record.Exception(() => new Cpf(cpf));

        // assert
        exception.Should().NotBeNull();
        exception.Should().As<DomainException>();
    }

    [Theory]
    [InlineData("28078729480", "280.787.294-80")]
    [InlineData("431.155.938-05", "431.155.938-05")]
    [InlineData("647.338.988-19", "647.338.988-19")]
    [InlineData("79284661471", "792.846.614-71")]
    [InlineData("63785815417", "637.858.154-17")]
    public void ToString_Should_ReturnCpfFormated_When_CpfNumber(string cpfNumber, string expected)
    {
        // act
        var cpfFormated = new Cpf(cpfNumber).ToString();

        // assert
        cpfFormated.Should().NotBeNullOrEmpty();
        cpfFormated.Should().Be(expected);
    }

    [Theory]
    [InlineData("148.550.770-70", "148.550.770-70")]
    [InlineData("06802626012", "068.026.260-12")]
    [InlineData("733054.944-41", "733054.944-41")]
    [InlineData("016790664-04", "016790664-04")]
    public void Equals_Should_ReturnTrue_When_TwoEqualCpfNumbers(string cpf, string expected)
    {
        // arrange
        var cpfTest = new Cpf(cpf);
        var cpfExpected = new Cpf(expected);

        // act
        var result = cpfTest.Equals(cpfExpected);

        // assert
        result.Should().BeTrue();
    }
}
