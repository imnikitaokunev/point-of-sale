using PointOfSale.Calculation;
using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests;

public class PointOfSaleTerminalShould
{
    public static Product A = new("A");
    public static Product B = new("B");
    public static Product C = new("C");
    public static Product D = new("D");

    public List<Price> Prices = new()
    {
        new UnitAndVolumePrice(A, 1.25, new VolumePrice(A, 3, 3)),
        new UnitPrice(B, 4.25),
        new UnitAndVolumePrice(C, 1, new VolumePrice(C, 5, 6)),
        new UnitPrice(D, 0.75)
    };

    public List<Price> AlmostZeroPrices = new()
    {
        new UnitAndVolumePrice(A, double.Epsilon, new VolumePrice(A, double.Epsilon, 1)),
        new UnitPrice(B, double.Epsilon),
        new UnitAndVolumePrice(C, double.Epsilon, new VolumePrice(C, double.Epsilon, 1)),
        new UnitPrice(D, double.Epsilon)
    };

    [Theory]
    [InlineData(13.25, "A", "A", "A", "A", "B", "C", "D", "A", "A", "A")]
    [InlineData(6, "C", "C", "C", "C", "C", "C", "C")]
    [InlineData(7.25, "A", "B", "C", "D")]
    [InlineData(0)]
    public void CalculateTotalPrice(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new PriceCalculator());

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "A", "A", "A", "A", "B", "C", "D", "A", "A", "A")]
    [InlineData(1, "C", "C", "C", "C", "C", "C", "C")]
    [InlineData(1, "A", "B", "C", "D")]
    public void UpdatePricingAndCalculateNonZeroTotalPrice(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(AlmostZeroPrices), new PriceCalculator());

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        terminal.SetPricing(new PriceStorage(Prices));

        var result = terminal.CalculateTotal();

        Assert.NotEqual(expected, result);
    }


    [Theory]
    [InlineData("I am uknown product")]
    public void ThrowAnExceptionForUnknownProductPrice(string product)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new PriceCalculator());

        Assert.Throws<UnknownPriceException>(() => terminal.Scan(product));
    }
}
