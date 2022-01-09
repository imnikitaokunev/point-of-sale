namespace PointOfSale.Storing;

public interface IProductProvider<TCode> where TCode : class
{
    IProduct CreateProduct(TCode code);
}
