using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale.Calculation;

public interface IPriceCalculator
{
    double CalculateTotal(IEnumerable<IProduct> products, IPriceStorage priceStorage);
    double Calculate(IProduct product, int count, IPriceStorage priceStorage);
}
