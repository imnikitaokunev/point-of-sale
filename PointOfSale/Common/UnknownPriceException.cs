using PointOfSale.Storing;

namespace PointOfSale.Common;

public class UnknownPriceException : Exception
{
    public UnknownPriceException() : base($"Could not get price")
    {
    }

    public UnknownPriceException(IProduct product) : base($"Could not get price of {product}")
    {
    }
}
