using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Pricing;

public class VolumePriceShould
{
    private Product product = new("A");

    [Theory]
    [InlineData(4, 5, 20, 16)]
    [InlineData(5, 5, 27, 25)]
    [InlineData(10, 5, 4, 0)]
    public void ReturnVolumePriceForCount(double packPrice, int packSize, int count, double expected)
    {
        var volume = new VolumePrice(product, packPrice, packSize);

        Assert.Equal(expected, volume.GetFor(count));
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 0)]
    public void ThrowAnExceptionForZeroPriceOrPack(double packPrice, int packSize)
    {
        Assert.Throws<ArgumentException>(() => new VolumePrice(product, packPrice, packSize));
    }
}
