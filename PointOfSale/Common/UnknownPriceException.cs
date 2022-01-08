using PointOfSale.Storing;

namespace PointOfSale.Common;

public class UnknownPriceException : Exception
{
    public UnknownPriceException(string product) : base($"Could not get price of {product}")
    {
    }

    public UnknownPriceException(IProduct product) : this(product?.Code)
    {
    }
}
