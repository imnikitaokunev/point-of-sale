using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public class UnitAndVolumePrice : UnitPrice
{
    public VolumePrice VolumePrice { get; set; }

    public UnitAndVolumePrice(IProduct product, double unitPrice, VolumePrice volumePrice) : base(product, unitPrice)
    {
        Require.NotNull(volumePrice, nameof(volumePrice));
        Require.GreaterThanZero(unitPrice, nameof(unitPrice));
        VolumePrice = volumePrice;
    }

    public override double GetFor(int count)
    {
        return base.GetFor(count % VolumePrice.PackSize) + VolumePrice.GetFor(count);
    }
}
