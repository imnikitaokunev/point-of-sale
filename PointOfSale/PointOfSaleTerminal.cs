using PointOfSale.Calculation;
using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public class PointOfSaleTerminal
{
    protected IProductCart ProductCart;
    protected IPriceStorage PriceStorage;
    protected IPriceCalculator PriceCalculator;

    public PointOfSaleTerminal(IProductCart productCart, IPriceStorage priceStorage, IPriceCalculator priceCalculator)
    {
        SetCart(productCart);
        SetPricing(priceStorage);
        SetCalculator(priceCalculator);
    }

    public void SetCart(IProductCart productCart)
    {
        Require.NotNull(productCart, nameof(productCart));
        CheckCart(productCart);
        ProductCart = productCart;
    }

    public void SetPricing(IPriceStorage priceStorage)
    {
        Require.NotNull(priceStorage, nameof(priceStorage));
        PriceStorage = priceStorage;
    }

    public void SetCalculator(IPriceCalculator priceCalculator)
    {
        Require.NotNull(priceCalculator, nameof(priceCalculator));
        PriceCalculator = priceCalculator;
    }

    public void Scan(string code)
    {
        Require.NotNullOrEmpty(code);

        if (!PriceStorage.HasPriceOf(code))
        {
            throw new UnknownPriceException(code);
        }

        ProductCart.Add(code);
    }

    public IEnumerable<IProduct> GetProducts()
    {
        return ProductCart.GetProducts();
    }

    public IReadOnlyCollection<Price> GetPrices()
    {
        return PriceStorage.GetPrices();
    }

    public double CalculateTotal()
    {
        var products = ProductCart.GetProducts();
        return PriceCalculator.CalculateTotal(products, PriceStorage);
    }

    protected virtual void CheckCart(IProductCart productCart)
    {
        if (ProductCart is not null && !ProductCart.IsEmpty())
        {
            throw new InvalidOperationException("Cannot set new cart when current is not empty");
        }

        if (!productCart.IsEmpty())
        {
            throw new InvalidOperationException("Cannot set not empty cart");
        }
    }
}
