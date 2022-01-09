using PointOfSale.Calculation;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public class PointOfSaleTerminalDefault : PointOfSaleTerminal
{
    public PointOfSaleTerminalDefault(IProductCart productCart, IPriceStorage priceStorage, IPriceCalculator priceCalculator)
        : base(productCart, priceStorage, priceCalculator)
    {
    }

    public PointOfSaleTerminalDefault()
        : this(ProductFactory.CreateProductCart(), ProductFactory.CreatePriceStorage(), ProductFactory.CreatePriceCalculator())
    {
    }

    public void SetPricing(IEnumerable<Price> prices)
    {
        SetPricing(ProductFactory.CreatePriceStorage(prices));
    }
}
