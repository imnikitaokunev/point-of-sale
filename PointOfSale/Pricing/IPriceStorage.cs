using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public interface IPriceStorage
{
    IReadOnlyCollection<Price> GetPrices();
    Price GetPrice(IProduct product);
    bool HasPriceOf(IProduct product);
    bool HasPriceOf(string code);
}
