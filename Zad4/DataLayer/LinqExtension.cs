using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class LinqExtension
    {
    public static List<Product> GetProductsWithoutCategory(this List<Product> products)
    {
        return products.Where(p => p.ProductSubcategory == null || p.ProductSubcategoryID == null).ToList();
    }

    public static List<Product> AsPage(this List<Product> products, int page, int numOfProducts)
    {
        if (page < 1 || numOfProducts < 0)
        {
            throw new ArgumentException();
        }

        return products.Skip(numOfProducts * (page - 1)).Take(numOfProducts).ToList();
    }

    public static string AsString(this List<Product> products, List<ProductVendor> vendors)
    {
        StringBuilder stringBuilder = new StringBuilder();
        var query = products.Join(vendors, product => product.ProductID, vendor => vendor.ProductID, (product, vendor) => product.Name + "-" + vendor.Vendor.Name).ToList();

        foreach (var q in query)
        {
            stringBuilder.AppendLine(q);
        }
        return stringBuilder.ToString();
    }
}

