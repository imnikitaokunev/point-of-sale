using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale.Calculation;

public interface IPriceCalculator<TCode> where TCode : class
{
    double CalculateTotal(IEnumerable<IProduct> products, IPriceStorage<TCode> priceStorage);
    double Calculate(IProduct product, int count, IPriceStorage<TCode> priceStorage);
}
