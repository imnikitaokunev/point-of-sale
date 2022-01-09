using PointOfSale.Pricing;
using PointOfSale.Storing;

namespace PointOfSale.Tests;

public class TestData
{
    public static IProduct A = new Product("A");
    public static IProduct B = new Product("B");
    public static IProduct C = new Product("C");
    public static IProduct D = new Product("D");

    public static List<Price> Prices = new()
    {
        new UnitAndVolumePrice(A, 1.25, 3, 3),
        new UnitPrice(B, 4.25),
        new UnitAndVolumePrice(C, 1, 6, 5),
        new UnitPrice(D, 0.75)
    };

    public static List<Price> AlmostZeroPrices = new()
    {
        new UnitPrice(A, double.Epsilon),
        new VolumePrice(B, 2, double.Epsilon),
        new UnitPrice(C, double.Epsilon),
        new VolumePrice(D, 1, double.Epsilon)
    };

    public static List<Price> UniquePrices = new()
    {
        new UnitPrice(A, 4.25),
        new VolumePrice(B, 5, 6),
        new UnitAndVolumePrice(C, 1, 6, 5),
    };

    public static List<Price> NullPrices = new()
    {
        null,
        null
    };
}
