using PointOfSale.Common;
using PointOfSale.Pricing;

namespace PointOfSale;

public class PointOfSaleTerminal
{
    private readonly List<string> _codes;
    private readonly Dictionary<string, Price> _prices;

    public PointOfSaleTerminal()
    {
        _codes = new List<string>();
        _prices = new Dictionary<string, Price>();
    }

    public void SetPricing(IEnumerable<Price> prices)
    {
        Require.NotNull(prices, nameof(prices));
        Require.NonNullElements(prices, nameof(prices));

        foreach(var price in prices)
        {
            _prices.Add(price.Code, price);
        }
    }

    public void Scan(string code)
    {
        Require.NotNullOrEmpty(code);

        if (!_prices.ContainsKey(code))
        {
            throw new UnknownPriceException();
        }

        _codes.Add(code);
    }

    public double CalculateTotal()
    {
        var groupedCodes = _codes.GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() });
        var totalPrice = default(double);
        
        foreach (var code in groupedCodes)
        {
            if (!_prices.TryGetValue(code.Key, out var price))
            {
                throw new UnknownPriceException(code.Key);
            }

            totalPrice += price.Of(code.Count);
        }

        return totalPrice;
    }
}
