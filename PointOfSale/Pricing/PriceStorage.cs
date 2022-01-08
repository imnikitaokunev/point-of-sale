using System.Collections.ObjectModel;
using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public sealed class PriceStorage : Dictionary<IProduct, Price>, IPriceStorage
{
    public PriceStorage(IEnumerable<Price> prices)
    {
        SetPricing(prices);
    }

    public IReadOnlyCollection<Price> GetPrices()
    {
        return new ReadOnlyCollection<Price>(Values.ToList());
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

    public bool HasPriceOf(IProduct product)
    {
        Require.NotNull(product, nameof(product));
        return ContainsKey(product);
    }

    public bool HasPriceOf(string code)
    {
        Require.NotNullOrEmpty(code, nameof(code));
        return Keys.Any(x => x.Equals(code));
    }

    private void SetPricing(IEnumerable<Price> prices)
    {
        Require.NonNullElements(prices, nameof(prices));

        foreach (var price in prices)
        {
            if (ContainsKey(price.Product))
            {
                throw new ArgumentException("An item with the same key has already been added.");
            }

            Add(price.Product, price);
        }
    }
}
