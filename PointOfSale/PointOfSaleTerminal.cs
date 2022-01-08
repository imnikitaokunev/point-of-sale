using PointOfSale.Calculation;
using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale;

public class PointOfSaleTerminal
{
    private IProductCart _productCart;
    private IPriceStorage _priceStorage;
    private IPriceCalculator _priceCalculator;

    public PointOfSaleTerminal(IProductCart productCart, IPriceStorage priceStorage, IPriceCalculator priceCalculator)
    {
        SetCart(productCart);
        SetPricing(priceStorage);
        SetCalculator(priceCalculator);
    }

    public void SetCart(IProductCart productCart)
    {
        Require.NotNull(productCart, nameof(productCart));
        _productCart = productCart;
    }

    public void SetPricing(IPriceStorage priceStorage)
    {
        Require.NotNull(priceStorage, nameof(priceStorage));
        _priceStorage = priceStorage;
    }

    public void SetCalculator(IPriceCalculator priceCalculator)
    {
        Require.NotNull(priceCalculator, nameof(priceCalculator));
        _priceCalculator = priceCalculator;
    }

    public void Scan(string code)
    {
        Require.NotNullOrEmpty(code);

        if (!_priceStorage.HasPriceOf(code))
        {
            throw new UnknownPriceException(code);
        }

        _productCart.Add(code);
    }

    public IEnumerable<IProduct> GetProducts()
    {
        return _productCart.GetProducts();
    }

    public IReadOnlyCollection<Price> GetPrices()
    {
        return _priceStorage.GetPrices();
    }

    public double CalculateTotal()
    {
        var products = _productCart.GetProducts();
        return _priceCalculator.CalculateTotal(products, _priceStorage);
    }
}
