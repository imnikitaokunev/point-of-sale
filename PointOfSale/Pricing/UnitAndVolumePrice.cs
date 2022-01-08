using PointOfSale.Common;
using PointOfSale.Storing;

namespace PointOfSale.Pricing;

public class UnitAndVolumePrice : UnitPrice
{
    public VolumePrice VolumePrice { get; set; }

    public UnitAndVolumePrice(IProduct product, double unitPrice, int packSize, double packPrice) : base(product, unitPrice)
    {
        Require.GreaterThanZero(unitPrice, nameof(unitPrice));
        VolumePrice = new VolumePrice(product, packSize, packPrice);
    }
}
