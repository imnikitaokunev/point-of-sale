using PointOfSale.Common;
using Xunit;

namespace PointOfSale.Tests;

public class PointOfSaleTerminalShould
{
    [Theory]
    [InlineData(13.25, "A", "A", "A", "A", "B", "C", "D", "A", "A", "A")]
    [InlineData(6, "C", "C", "C", "C", "C", "C", "C")]
    [InlineData(7.25, "A", "B", "C", "D")]
    [InlineData(7.25, "D", "C", "B", "A")]
    [InlineData(5, "C", "C", "C", "C", "C")]
    [InlineData(5, "C", "C", "C", "C", "C", "C")]
    [InlineData(1.25, "A")]
    [InlineData(4.25, "B")]
    [InlineData(1, "C")]
    [InlineData(0.75, "D")]
    [InlineData(50, "B", "B", "B", "B", "B", "B", "B", "B", "B", "B", "D", "D", "D", "D", "D", "D", "D", "D", "D", "D")] // 10 B + 10 D
    [InlineData(8, "A", "A", "A", "A", "D", "D", "D", "D", "D")] // 4 A + 5 D
    [InlineData(0)]
    public void CalculateTotalPrice(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(TestData.Prices);

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(200.50, "A", 200)]          // % 3 == 2
    [InlineData(300.00, "A", 300)]          // % 3 == 0
    [InlineData(400.25, "A", 400)]          // % 3 == 1
    [InlineData(42500, "B", 10_000)]
    [InlineData(425000, "B", 100_000)]
    [InlineData(5000, "C", 6_000)]          // % 6 == 0
    [InlineData(10_001, "C", 12_001)]       // % 6 == 1
    [InlineData(20_002, "C", 24_002)]       // % 6 == 2
    [InlineData(30_003, "C", 36_003)]       // % 6 == 3
    [InlineData(400_004, "C", 480_004)]     // % 6 == 4
    [InlineData(5_000_005, "C", 6_000_005)] // % 6 == 5
    public void CalculateTotalPriceStress(double expected, string code, int count)
    {
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(TestData.Prices);

        var products = Enumerable.Repeat(code, count);
        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("I am uknown product")]
    public void ThrowAnExceptionForUnknownProductPrice(string product)
    {
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(TestData.Prices);

        Assert.Throws<UnknownPriceException>(() => terminal.Scan(product));
    }
}

