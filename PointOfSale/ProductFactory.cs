using PointOfSale.Calculation;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public class ProductFactory
{
    public static IProductProvider CreateProductProvider()
    {
        return new ProductProvider();
    }

    public static IProductCart CreateProductCart()
    {
        return new ProductCart(CreateProductProvider());
    }

    public static IPriceCalculator CreatePriceCalculator()
    {
        return new PriceCalculator();
    }

    public static IPriceStorage CreatePriceStorage()
    {
        return new PriceStorage(Enumerable.Empty<Price>());
    }

    public static IPriceStorage CreatePriceStorage(IEnumerable<Price> prices)
    {
        return new PriceStorage(prices);
    }
}