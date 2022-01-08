namespace PointOfSale.Storing;

public class ProductProvider : IProductProvider
{
    public IProduct CreateProduct(string code)
    {
        return new Product(code);
    }
}
