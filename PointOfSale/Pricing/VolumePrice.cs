using PointOfSale.Common;

namespace PointOfSale.Pricing;

public class VolumePrice : Price
{
    public int PackSize { get; }
    public double PackPrice { get; }

    public VolumePrice(string code, int packSize, double packPrice) : base(code)
    {
        Require.GreaterThanZero(packSize, nameof(packSize));
        Require.GreaterThanZero(packPrice, nameof(packPrice));
        PackSize = packSize;
        PackPrice = packPrice;
    }

    public override double Of(int count)
    {
        if (count % PackSize != 0)
        {
            throw new ArgumentException();
        }

        return count / PackSize * PackPrice;
    }
}
