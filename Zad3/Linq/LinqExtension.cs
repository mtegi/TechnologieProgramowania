using Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   public static class LinqExtension
    {
    #region 4 deklaratywnie
    public static List<Product> GetProductsWithoutCategory_D(this List<Product> products)
    {
        return (from product in products
                where product.ProductSubcategory == null || product.ProductSubcategoryID == null
                select product).ToList();
    }

    public static string AsList_D(this List<Product> products, List<ProductVendor> vendors)
    {
        StringBuilder stringBuilder = new StringBuilder();
        var query = (from product in products
                      from vendor in vendors
                      where vendor.ProductID == product.ProductID
                      select product.Name + "-" + vendor.Vendor.Name).ToList();
        foreach(var q in query)
        {
            stringBuilder.AppendLine(q);
        }
        return stringBuilder.ToString();
    }
    #endregion
    #region 4 imperatywnie
    public static List<Product> GetProductsWithoutCategory_I(this List<Product> products)
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

    public static string AsList_I(this List<Product> products, List<ProductVendor> vendors)
    {
        StringBuilder stringBuilder = new StringBuilder();
        var query = products.Join(vendors, product => product.ProductID, vendor => vendor.ProductID, (product, vendor) => product.Name + "-" + vendor.Vendor.Name).ToList();

        foreach (var q in query)
        {
            stringBuilder.AppendLine(q);
        }
        return stringBuilder.ToString();
    }
    #endregion
}

