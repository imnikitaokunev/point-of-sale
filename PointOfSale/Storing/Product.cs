namespace PointOfSale.Storing;

public class Product : IProduct
{
    public string Code { get; }

    public Product(string code)
    {
        Code = code;
    }

    public override bool Equals(object obj)
    {
        if (obj is Product product)
        {
            return Code.Equals(product.Code);
        }

        if (obj is string code)
        {
            return Code.Equals(code);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
}
