using PointOfSale.Calculation;
using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Calculation;

public class PriceCalculatorShould
{
    [Fact]
    public void CalculatePriceForSingleTypeProduct()
    {
        IPriceCalculator calculator = new PriceCalculator();
        IPriceStorage storage = new PriceStorage(TestData.Prices);

        Assert.Equal(10.25, calculator.Calculate(TestData.A, 10, storage));
        Assert.Equal(17, calculator.Calculate(TestData.B, 4, storage));
        Assert.Equal(6, calculator.Calculate(TestData.C, 7, storage));
    }

    [Fact]
    public void CalculateTotalPriceForProductsCollection()
    {
        IPriceCalculator calculator = new PriceCalculator();
        IPriceStorage storage = new PriceStorage(TestData.Prices);

        Assert.Equal(16.5, calculator.CalculateTotal(new IProduct[] { 
            TestData.A, TestData.A, TestData.A,
            TestData.B, TestData.B,
            TestData.C, TestData.C, TestData.C, TestData.C, TestData.C, TestData.C }
        , storage));
    }
}
