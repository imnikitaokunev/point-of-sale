using PointOfSale.Common;

namespace PointOfSale.Pricing;

public class UnitAndVolumePrice : UnitPrice
{
    public VolumePrice VolumePrice { get; set; }

    public UnitAndVolumePrice(string code, double unitPrice, int packSize, double packPrice) : base(code, unitPrice)
    {
        Require.GreaterThanZero(unitPrice, nameof(unitPrice));
        VolumePrice = new VolumePrice(code, packSize, packPrice);
    }

    public override double Of(int count)
    {
        var units = count % VolumePrice.PackSize;

        var volumePrice = VolumePrice.Of(count - units);
        var unitPrice = base.Of(units);

        return unitPrice + volumePrice;
    }
}
