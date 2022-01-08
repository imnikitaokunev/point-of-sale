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
        Require.NotNull(priceStorage);
        _priceStorage = priceStorage;
    }

    public void SetCalculator(IPriceCalculator priceCalculator)
    {
        Require.NotNull(priceCalculator, nameof(priceCalculator));
        _priceCalculator = priceCalculator;
    }

    public void Scan(string product)
    {
        Require.NotNullOrEmpty(product);

        if (!_priceStorage.HasPriceOf(product))
        {
            throw new UnknownPriceException(product);
        }

        _productCart.Add(product);
    }

    public IEnumerable<IProduct> GetProducts()
    {
        return _productCart.GetProducts();
    }

    public double CalculateTotal()
    {
        var products = _productCart.GetProducts();
        return _priceCalculator.CalculateTotal(products, _priceStorage);
    }
}
