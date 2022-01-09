namespace PointOfSale.Storing;

public interface IProductCart<TCode> where TCode : class
{
    void Add(TCode code);
    IEnumerable<IProduct> GetProducts();
    bool IsEmpty();
}
