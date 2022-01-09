namespace PointOfSale.Storing;

public class ProductProvider : IProductProvider<string>
{
    public IProduct CreateProduct(string code)
    {
        return new Product(code);
    }
}
