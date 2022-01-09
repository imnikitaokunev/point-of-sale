using PointOfSale.Storing;
using Xunit;

namespace PointOfSale.Tests.Storing;

public class ProductCartShould
{
    [Theory]
    [InlineData("Product")]
    [InlineData("Another Product")]
    public void AddProductByName(string name)
    {
        IProductCart<string> productCart = new ProductCart(new ProductProvider());
        productCart.Add(name);

        Assert.NotEmpty(productCart.GetProducts());
    }

    [Theory]
    [InlineData("Product", "Product1", "Product2")]
    [InlineData("A", "B", "C", "D", "E")]
    [InlineData("A", "A", "A", "A", "A")]
    public void ReturnCollectionOfProducts(params string[] names)
    {
        IProductCart<string> productCart = new ProductCart(new ProductProvider());
        foreach(var name in names)
        {
            productCart.Add(name);
        }

        var products = productCart.GetProducts();
        Assert.Equal(names.Count(), products.Count());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false, "A")]
    public void ReturnIsCartEmpty(bool expected, params string[] names)
    {
        IProductCart<string> productCart = new ProductCart(new ProductProvider());
        foreach (var name in names)
        {
            productCart.Add(name);
        }

        Assert.Equal(expected, productCart.IsEmpty());
    }
}
