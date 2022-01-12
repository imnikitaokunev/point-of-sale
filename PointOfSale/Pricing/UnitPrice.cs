using PointOfSale.Common;

namespace PointOfSale.Pricing;

public class UnitPrice : Price
{
    public double PricePerUnit { get; }

    public UnitPrice(string code, double price) : base(code)
    {
        Require.GreaterThanZero(price, nameof(price));
        PricePerUnit = price;
    }

    public override double Of(int count)
    {
        return PricePerUnit * count;
    }
}
