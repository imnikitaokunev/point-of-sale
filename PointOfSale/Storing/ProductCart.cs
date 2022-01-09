namespace PointOfSale.Storing;

public class ProductCart : List<IProduct>, IProductCart
{
    private readonly IProductProvider _productProvider;

    public ProductCart(IProductProvider productProvider)
    {
        _productProvider = productProvider;
    }

    public void Add(string code)
    {
        Add(_productProvider.CreateProduct(code));
    }

    public IEnumerable<IProduct> GetProducts()
    {
        return this.ToList();
    }

    public bool IsEmpty()
    {
        return Count == 0;
    }
}
