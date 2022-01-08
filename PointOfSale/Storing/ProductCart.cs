﻿namespace PointOfSale.Storing;

public class ProductCart : List<IProduct>, IProductCart
{
    private readonly IProductProvider _productProvider;

    public ProductCart(IProductProvider productProvider)
    {
        _productProvider = productProvider;
    }

    public void Add(string name)
    {
        Add(_productProvider.CreateProduct(name));
    }

    public IEnumerable<IProduct> GetProducts()
    {
        return this.ToList();
    }
}
