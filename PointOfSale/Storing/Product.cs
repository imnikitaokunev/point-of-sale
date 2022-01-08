namespace PointOfSale.Storing;

public class Product : IProduct
{
    public string Name { get; }

    public Product(string name)
    {
        Name = name;
    }

    public override bool Equals(object obj)
    {
        if (obj is null || obj is not Product product)
        {
            return false;
        }

        return Name.Equals(product.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
