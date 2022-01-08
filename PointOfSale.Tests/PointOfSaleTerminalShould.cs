using PointOfSale.Calculation;
using PointOfSale.Common;
using PointOfSale.Pricing;
using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests;

public class PointOfSaleTerminalShould
{
    #region Test Information

    private static Product A = new("A");
    private static Product B = new("B");
    private static Product C = new("C");
    private static Product D = new("D");

    private List<Price> Prices = new()
    {
        new UnitAndVolumePrice(A, 1.25, 3, 3),
        new UnitPrice(B, 4.25),
        new UnitAndVolumePrice(C, 1, 6, 5),
        new UnitPrice(D, 0.75)
    };

    private List<Price> AlmostZeroPrices = new()
    {
        new UnitPrice(A, double.Epsilon),
        new VolumePrice(B, 2, double.Epsilon),
        new UnitPrice(C, double.Epsilon),
        new VolumePrice(D, 1, double.Epsilon)
    };

    #endregion

    [Theory]
    [InlineData(13.25, "A", "A", "A", "A", "B", "C", "D", "A", "A", "A")]
    [InlineData(6, "C", "C", "C", "C", "C", "C", "C")]
    [InlineData(7.25, "A", "B", "C", "D")]
    [InlineData(5, "C", "C", "C", "C", "C")]
    [InlineData(5, "C", "C", "C", "C", "C", "C")]
    [InlineData(1, "C")]
    [InlineData(50, "B", "B", "B", "B", "B", "B", "B", "B", "B", "B", "D", "D", "D", "D", "D", "D", "D", "D", "D", "D")] // 10 B + 10 D
    [InlineData(8, "A", "A", "A", "A", "D", "D", "D", "D", "D")] // 4 A + 5 D
    [InlineData(0)]
    public void CalculateTotalPrice(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new PriceCalculator());

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(200.50, "A", 200)]          // % 3 == 2
    [InlineData(300.00, "A", 300)]          // % 3 == 0
    [InlineData(400.25, "A", 400)]          // % 3 == 1
    [InlineData(42500, "B", 10_000)]
    [InlineData(425000, "B", 100_000)]
    [InlineData(5000, "C", 6_000)]          // % 6 == 0
    [InlineData(10_001, "C", 12_001)]       // % 6 == 1
    [InlineData(20_002, "C", 24_002)]       // % 6 == 2
    [InlineData(30_003, "C", 36_003)]       // % 6 == 3
    [InlineData(400_004, "C", 480_004)]     // % 6 == 4
    [InlineData(5_000_005, "C", 6_000_005)] // % 6 == 5
    public void CalculateTotalPriceStress(double expected, string code, int count)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new PriceCalculator());

        var products = Enumerable.Repeat(code, count);
        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "A", "A", "A", "A", "B", "C", "D", "A", "A", "A")]
    [InlineData(1, "C", "C", "C", "C", "C", "C", "C")]
    [InlineData(1, "A", "B", "C", "D")]
    public void UpdatePricingAndCalculateNonZeroTotalPrice(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(AlmostZeroPrices), new PriceCalculator());

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        terminal.SetPricing(new PriceStorage(Prices));

        var result = terminal.CalculateTotal();

        Assert.True(result > expected);
    }

    [Theory]
    [InlineData(200, "A", "A", "A", "A", "B", "C", "D", "A", "A", "A")]
    [InlineData(140, "C", "C", "C", "C", "C", "C", "C")]
    [InlineData(80, "A", "B", "C", "D")]
    public void UpdatePricesAndCalculateNonZeroTotalPrice(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(AlmostZeroPrices), new PriceCalculator());

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var oldPrices = terminal.GetPrices();
        var newPrices = oldPrices
            .OfType<UnitPrice>().Select(x => new UnitPrice(x.Product, 20))
            .Union<Price>(oldPrices
                .OfType<VolumePrice>().Select(x => new VolumePrice(x.Product, 1, 20)))
            .ToList();

        terminal.SetPricing(new PriceStorage(newPrices));

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("I am uknown product")]
    public void ThrowAnExceptionForUnknownProductPrice(string product)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new PriceCalculator());

        Assert.Throws<UnknownPriceException>(() => terminal.Scan(product));
    }

    [Theory]
    [InlineData(2.5, "A")]
    [InlineData(17, "B", "B")]
    [InlineData(5, "C", "C", "C")]
    public void CalculateTotalPriceForDifferentCarts(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new PriceCalculator());
        terminal.SetCart(new AbsolutelyMagicallyDuplicateProductCart(new ProductProvider()));

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3.75, "A", "A", "A")]
    [InlineData(12, "C", "C", "C", "C", "C", "C", "C", "C", "C", "C", "C", "C")] //12 C
    public void CalculateTotalPriceUsingDifferentCalculators(double expected, params string[] products)
    {
        var terminal = new PointOfSaleTerminal(new ProductCart(new ProductProvider()), new PriceStorage(Prices), new KnowingNothingAboutVolumePricesCalculator());

        foreach (var product in products)
        {
            terminal.Scan(product);
        }

        terminal.SetPricing(new PriceStorage(Prices));

        var result = terminal.CalculateTotal();

        Assert.Equal(expected, result);
    }

    #region Test classes

    private class AbsolutelyMagicallyDuplicateProductCart : List<IProduct>, IProductCart
    {
        private readonly IProductProvider _productProvider;

        public AbsolutelyMagicallyDuplicateProductCart(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        public void Add(string name)
        {
            Add(_productProvider.CreateProduct(name));
            Add(_productProvider.CreateProduct(name));
        }

        public IEnumerable<IProduct> GetProducts()
        {
            return this.ToList();
        }
    }

    private class KnowingNothingAboutVolumePricesCalculator : IPriceCalculator
    {
        private readonly IDictionary<Type, Func<Price, int, double>> _priceHandlers;

        public KnowingNothingAboutVolumePricesCalculator()
        {
            _priceHandlers = new Dictionary<Type, Func<Price, int, double>>
            {
                {typeof(UnitPrice), CalculateUnitPrice},
                {typeof(UnitAndVolumePrice), CalculateUnitPrice},
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
    }

    #endregion
}

