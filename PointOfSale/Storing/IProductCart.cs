namespace PointOfSale.Storing;

public interface IProductCart
{
    void Add(string name);
    void Add(IProduct product);
    IEnumerable<IProduct> GetProducts();
}
