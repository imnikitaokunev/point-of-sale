using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public interface IPriceStorage
{
    void SetPricing(IEnumerable<Price> prices);
    Price GetPrice(IProduct product);
    bool HasPriceOf(string name);
}
