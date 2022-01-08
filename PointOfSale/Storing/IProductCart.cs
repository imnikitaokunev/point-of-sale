namespace PointOfSale.Storing;

public interface IProductCart
{
    void Add(string code);
    IEnumerable<IProduct> GetProducts();
}
