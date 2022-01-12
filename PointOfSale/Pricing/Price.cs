using PointOfSale.Common;

namespace PointOfSale.Pricing;

public abstract class Price
{
    public string Code { get; }

    public Price(string code)
    {
        Require.NotNullOrEmpty(code, nameof(code));
        Code = code;
    }

    public abstract double Of(int count);
}
