using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Pricing;

public class UnitAndVolumePriceShould
{
    private Product product = new("A");

    [Theory]
    [InlineData(1.25, 3, 3, 10, 10.25)]
    [InlineData(10, 5, 45, 4, 40)]
    [InlineData(5, 14, 3, 3, 14)]
    public void ReturnTotalPriceForCount(double unitPrice, double packPrice, int packSize, int count, double expected)
    {
        var price = new UnitAndVolumePrice(product, unitPrice, new VolumePrice(product, packPrice, packSize));

        Assert.Equal(expected, price.GetFor(count));
    }

    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, 1)]
    [InlineData(0, 0, 1)]
    [InlineData(0, 1, 0)]
    [InlineData(1, 0, 0)]
    [InlineData(0, 0, 0)]
    public void ThrowAnExceptionForZeroPricesOrPack(double unitPrice, double packPrice, int packSize)
    {
        Assert.Throws<ArgumentException>(() => new UnitAndVolumePrice(product, unitPrice, new VolumePrice(product, packPrice, packSize)));
    }
}
