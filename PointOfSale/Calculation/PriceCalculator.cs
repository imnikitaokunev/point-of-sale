using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale.Calculation;

public class PriceCalculator : IPriceCalculator
{
    public double CalculateTotal(IEnumerable<IProduct> products, IPriceStorage priceStorage)
    {
        var groupedProducts = products.GroupBy(x => x.Name).Select(g => new { Product = g.FirstOrDefault(), Count = g.Count() });

        var totalPrice = default(double);
        foreach (var group in groupedProducts)
        {
            var price = priceStorage.GetPrice(group.Product);
            totalPrice += price.GetFor(group.Count);
        }

        return totalPrice;
    }
}
