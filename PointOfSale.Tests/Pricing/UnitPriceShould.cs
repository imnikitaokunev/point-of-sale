using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Pricing;

public class UnitPriceShould
{
    private Product product = new("A");

    [Theory]
    [InlineData(4, 5, 20)]
    [InlineData(2.5, 10, 25)]
    [InlineData(7, 0, 0)]
    public void ReturnUnitPriceForCount(double price, int count, double expected)
    {
        var unit = new UnitPrice(product, price);

        Assert.Equal(expected, unit.GetFor(count));
    }

    [Theory]
    [InlineData(0)]
    public void ThrowAnExceptionForZeroPrice(double price)
    {
        Assert.Throws<ArgumentException>(() => new UnitPrice(product, price));
    }
}
