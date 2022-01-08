using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public class VolumePrice : Price
{
    public int PackSize { get; }
    public double PackPrice { get; }

    public VolumePrice(IProduct product, int packSize, double packPrice) : base(product)
    {
        Require.GreaterThanZero(packSize, nameof(packSize));
        Require.GreaterThanZero(packPrice, nameof(packPrice));
        PackSize = packSize;
        PackPrice = packPrice;
    }
}
