namespace PointOfSale.Storing;

public class ProductProvider : IProductProvider
{
    public IProduct CreateProduct(string name)
    {
        return new Product(name);
    }
}
