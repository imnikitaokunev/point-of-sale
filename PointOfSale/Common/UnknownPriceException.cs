using PointOfSale.Storing;

namespace PointOfSale.Common;

public class UnknownPriceException : Exception
{
    public UnknownPriceException(string code) : base($"Could not get price of {code}")
    {
    }

    public UnknownPriceException(IProduct product) : this($"{product}")
    {
    }
}
