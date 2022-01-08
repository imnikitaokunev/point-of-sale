using PointOfSale.Calculation;
using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Calculation;

public class PriceCalculatorShould
{
    private static IProduct A = new Product("A");
    private static IProduct B = new Product("B");
    private static IProduct C = new Product("C");

    private static Price UnitPrice = new UnitPrice(A, 10);
    private static Price VolumePrice = new VolumePrice(B, 2, 5);
    private static Price UnitAndVolumePrice = new UnitAndVolumePrice(C, 10, 5, 40);

    private IEnumerable<Price> Prices = new List<Price>()
    {
       UnitPrice,
       VolumePrice,
       UnitAndVolumePrice
    };

    [Fact]
    public void CalculatePriceForSingleTypeProduct()
    {
        IPriceCalculator calculator = new PriceCalculator();
        IPriceStorage storage = new PriceStorage(Prices);

        Assert.Equal(100, calculator.Calculate(A, 10, storage));
        Assert.Equal(10, calculator.Calculate(B, 4, storage));
        Assert.Equal(60, calculator.Calculate(C, 7, storage));
    }

    [Fact]
    public void CalculateTotalPriceForProductsCollection()
    {
        IPriceCalculator calculator = new PriceCalculator();
        IPriceStorage storage = new PriceStorage(Prices);

        Assert.Equal(85, calculator.CalculateTotal(new IProduct[] { A, A, A, B, B, C, C, C, C, C, C }, storage));
    }
}
