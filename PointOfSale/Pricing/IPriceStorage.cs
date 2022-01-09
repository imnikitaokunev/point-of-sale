using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public interface IPriceStorage<TCode> where TCode : class
{
    IReadOnlyCollection<Price> GetPrices();
    Price GetPrice(IProduct product);
    bool HasPriceOf(IProduct product);
    bool HasPriceOf(TCode code);
}
