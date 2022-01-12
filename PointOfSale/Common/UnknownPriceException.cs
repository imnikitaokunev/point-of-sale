namespace PointOfSale.Common;

public class UnknownPriceException : Exception
{
    public UnknownPriceException() : base($"Could not get price")
    {
    }

    public UnknownPriceException(string code) : base($"Could not get price of {code}")
    {
    }
}
