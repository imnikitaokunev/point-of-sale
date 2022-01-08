using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public class VolumePrice : Price
{
    public double PackPrice { get; }
    public int PackSize { get; }

    public VolumePrice(IProduct product, double packPrice, int packSize) : base(product)
    {
        Require.GreaterThanZero(packPrice, nameof(packPrice));
        Require.GreaterThanZero(packSize, nameof(packSize));
        PackPrice = packPrice;
        PackSize = packSize;
    }

    public override double GetFor(int count)
    {
        var packs = count / PackSize;
        return packs * PackPrice;
    }
}
