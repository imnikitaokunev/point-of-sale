using PointOfSale.Calculation;
using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public abstract class PointOfSaleTerminalBase<TCode> where TCode : class
{
    protected IProductCart<TCode> ProductCart;
    protected IPriceStorage<TCode> PriceStorage;
    protected IPriceCalculator<TCode> PriceCalculator;

    public PointOfSaleTerminalBase(IProductCart<TCode> productCart, IPriceStorage<TCode> priceStorage, IPriceCalculator<TCode> priceCalculator)
    {
        SetCart(productCart);
        SetPricing(priceStorage);
        SetCalculator(priceCalculator);
    }

    public void SetCart(IProductCart<TCode> productCart)
    {
        Require.NotNull(productCart, nameof(productCart));
        CheckCart(productCart);
        ProductCart = productCart;
    }

    public void SetPricing(IPriceStorage<TCode> priceStorage)
    {
        Require.NotNull(priceStorage, nameof(priceStorage));
        PriceStorage = priceStorage;
    }

    public void SetCalculator(IPriceCalculator<TCode> priceCalculator)
    {
        Require.NotNull(priceCalculator, nameof(priceCalculator));
        PriceCalculator = priceCalculator;
    }

    public void Scan(TCode code)
    {
        Require.NotNull(code);

        if (!PriceStorage.HasPriceOf(code))
        {
            throw new UnknownPriceException();
        }

        ScanInternal(code);

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

    private void CheckCart(IProductCart<TCode> productCart)
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

    protected abstract void ScanInternal(TCode code);
}
