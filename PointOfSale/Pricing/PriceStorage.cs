using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public sealed class PriceStorage : Dictionary<IProduct, Price>, IPriceStorage
{
    public PriceStorage(IEnumerable<Price> prices)
    {
        SetPricing(prices);
    }

    public void SetPricing(IEnumerable<Price> prices)
    {
        foreach (var price in prices)
        {
            if (ContainsKey(price.Product))
            {
                Remove(price.Product);
            }

            Add(price.Product, price);
        }
    }

    public Price GetPrice(IProduct product)
    {
        Require.NotNull(product, nameof(product));

        if (!TryGetValue(product, out var price))
        {
            throw new KeyNotFoundException();
        }

        return price;
    }

    public bool HasPriceOf(string name)
    {
        Require.NotNullOrEmpty(name, nameof(name));
        return Keys.Any(x => x.Name.Equals(name));
    }
}
