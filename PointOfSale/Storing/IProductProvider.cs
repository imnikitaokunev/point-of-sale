namespace PointOfSale.Storing;

public interface IProductProvider
{
    IProduct CreateProduct(string name);
}
