using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public abstract class Price
{
    public IProduct Product { get; }

    public Price(IProduct product)
    {
        Require.NotNull(product, nameof(product));
        Product = product;
    }
}
