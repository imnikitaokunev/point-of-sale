using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale.Calculation;

public class PriceCalculator : IPriceCalculator
{
    private readonly IDictionary<Type, Func<Price, int, double>> _priceHandlers;

    public PriceCalculator()
    {
        _priceHandlers = new Dictionary<Type, Func<Price, int, double>>
        {
            {typeof(UnitPrice), CalculateUnitPrice},
            {typeof(VolumePrice), CalculateVolumePrice},
            {typeof(UnitAndVolumePrice), CalculateUnitAndVolumePrice},
        };
    }

    public double CalculateTotal(IEnumerable<IProduct> products, IPriceStorage priceStorage)
    {
        var groupedProducts = products.GroupBy(x => x);
        var totalPrice = default(double);

        foreach (var group in groupedProducts)
        {
            totalPrice += Calculate(group.Key, group.Count(), priceStorage);
        }

        return totalPrice;
    }

    public double Calculate(IProduct product, int count, IPriceStorage priceStorage)
    {
        if (!priceStorage.HasPriceOf(product))
        {
            throw new UnknownPriceException(product);
        }
        
        var price = priceStorage.GetPrice(product);
        if (!_priceHandlers.TryGetValue(price.GetType(), out var priceHandler))
        {
            throw new KeyNotFoundException();
        }

        return priceHandler(price, count);
    }

    private double CalculateUnitPrice(Price price, int count)
    {
        if (price is not UnitPrice unitPrice)
        {
            throw new ArgumentException($"Argument of type {typeof(UnitPrice)} expected");
        }

        return unitPrice.PricePerUnit * count;
    }

    private double CalculateVolumePrice(Price price, int count)
    {
        if (price is not VolumePrice volumePrice)
        {
            throw new ArgumentException($"Argument of type {typeof(VolumePrice)} expected");
        }

        var packs = count / volumePrice.PackSize;
        return packs * volumePrice.PackPrice;
    }

    private double CalculateUnitAndVolumePrice(Price price, int count)
    {
        if (price is not UnitAndVolumePrice unitAndvolumePrice)
        {
            throw new ArgumentException($"Argument of type {typeof(UnitAndVolumePrice)} expected");
        }

        var packs = count / unitAndvolumePrice.VolumePrice.PackSize;
        var units = count % unitAndvolumePrice.VolumePrice.PackSize;

        var volumePrice = packs * unitAndvolumePrice.VolumePrice.PackPrice;
        var unitPrice = units * unitAndvolumePrice.PricePerUnit;

        return unitPrice + volumePrice;
    }
}
