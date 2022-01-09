using PointOfSale.Calculation;
using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public class PointOfSaleTerminal : PointOfSaleTerminalBase<string>
{
    public PointOfSaleTerminal(IProductCart<string> productCart, IPriceStorage<string> priceStorage, IPriceCalculator<string> priceCalculator)
        : base(productCart, priceStorage, priceCalculator)
    {
    }

    public PointOfSaleTerminal()
        : this(ProductFactory.CreateProductCart(), ProductFactory.CreatePriceStorage(), ProductFactory.CreatePriceCalculator())
    {
    }

    public void SetPricing(IEnumerable<Price> prices)
    {
        SetPricing(ProductFactory.CreatePriceStorage(prices));
    }

    protected override void ScanInternal(string code)
    {
        Require.NotNullOrEmpty(code);
    }
}
