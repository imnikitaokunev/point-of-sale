using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Pricing;

public class PriceStorageShould
{
    private static IProduct A = new Product("A");
    private static IProduct B = new Product("B");
    private static IProduct C = new Product("C");

    private static Price UnitPrice = new UnitPrice(A, 10);
    private static Price VolumePrice = new VolumePrice(B, 30, 30);
    private static Price UnitAndVolumePrice = new UnitAndVolumePrice(C, 10, 5, 40);

    private IEnumerable<Price> Prices = new List<Price>()
    {
       UnitPrice,
       VolumePrice,
       UnitAndVolumePrice
    };

    [Theory]
    [InlineData(true, "A")]
    [InlineData(true, "B")]
    [InlineData(true, "C")]
    [InlineData(false, "D")]
    [InlineData(false, "AA")]
    [InlineData(false, "CB")]
    public void ReturnIsProductPricePresented(bool expected, string code)
    {
        IPriceStorage storage = new PriceStorage(Prices);
        var product = new Product(code);

        Assert.Equal(expected, storage.HasPriceOf(code));
        Assert.Equal(expected, storage.HasPriceOf(product));
    }

    [Fact]
    public void ReturnPriceOfKnownProduct()
    {
        IPriceStorage storage = new PriceStorage(Prices);

        Assert.Equal(UnitPrice, (UnitPrice)storage.GetPrice(A));
        Assert.Equal(VolumePrice, (VolumePrice)storage.GetPrice(B));
        Assert.Equal(UnitAndVolumePrice, (UnitAndVolumePrice)storage.GetPrice(C));
    }

    [Fact]
    public void ReturnPricesCollection()
    {
        IPriceStorage storage = new PriceStorage(Prices);

        var pricesCollection = storage.GetPrices();

        Assert.All(Prices, x => pricesCollection.Contains(x));
    }
}
