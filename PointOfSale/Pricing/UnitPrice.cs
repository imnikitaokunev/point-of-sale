using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public class UnitPrice : Price
{
    public double PricePerUnit { get; }

    public UnitPrice(IProduct product, double price) : base(product)
    {
        Require.GreaterThanZero(price, nameof(price));
        PricePerUnit = price;
    }
}
