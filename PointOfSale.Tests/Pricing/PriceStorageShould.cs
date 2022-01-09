using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Pricing;

public class PriceStorageShould
{
    [Theory]
    [InlineData(true, "A")]
    [InlineData(true, "B")]
    [InlineData(true, "C")]
    [InlineData(false, "D")]
    [InlineData(false, "AA")]
    [InlineData(false, "CB")]
    public void ReturnIsProductPricePresented(bool expected, string code)
    {
        IPriceStorage<string> storage = new PriceStorage(TestData.UniquePrices);
        var product = new Product(code);

        Assert.Equal(expected, storage.HasPriceOf(code));
        Assert.Equal(expected, storage.HasPriceOf(product));
    }

    [Fact]
    public void ReturnPriceOfKnownProduct()
    {
        IPriceStorage<string> storage = new PriceStorage(TestData.UniquePrices);

        Assert.Equal(TestData.UniquePrices[0], (UnitPrice)storage.GetPrice(TestData.A));
        Assert.Equal(TestData.UniquePrices[1], (VolumePrice)storage.GetPrice(TestData.B));
        Assert.Equal(TestData.UniquePrices[2], (UnitAndVolumePrice)storage.GetPrice(TestData.C));
    }

    [Fact]
    public void ReturnPricesCollection()
    {
        IPriceStorage<string> storage = new PriceStorage(TestData.Prices);

        var pricesCollection = storage.GetPrices();

        Assert.All(TestData.Prices, x => pricesCollection.Contains(x));
    }

    [Fact]
    public void ThrowAnExceptionWhenTryingToAddNullPrices()
    {
        Assert.Throws<ArgumentException>(() => new PriceStorage(TestData.NullPrices));
    }
}
