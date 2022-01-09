using PointOfSale.Calculation;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public class ProductFactory
{
    public static IProductProvider<string> CreateProductProvider()
    {
        return new ProductProvider();
    }

    public static IProductCart<string> CreateProductCart()
    {
        return new ProductCart(CreateProductProvider());
    }

    public static IPriceCalculator<string> CreatePriceCalculator()
    {
        return new PriceCalculator();
    }

    public static IPriceStorage<string> CreatePriceStorage()
    {
        return new PriceStorage(Enumerable.Empty<Price>());
    }

    public static IPriceStorage<string> CreatePriceStorage(IEnumerable<Price> prices)
    {
        return new PriceStorage(prices);
    }
}
