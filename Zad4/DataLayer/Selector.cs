using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class Selector
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                List<Product> result = (from product in data.Products
                                        where product.Name.Contains(namePart)
                                        select product).ToList();
                return result;
            }
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                List<Product> result = (from productVendor in data.ProductVendors
                                        where productVendor.Vendor.Name == vendorName
                                        select productVendor.Product).ToList();
                return result;
            }
        }



        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                List<string> result = (from productVendor in data.ProductVendors
                                       where productVendor.Vendor.Name.Equals(vendorName)
                                       select productVendor.Product.Name).ToList();
                return result;
            }
        }


        public static string GetProductVendorByProductName(string productName)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                string result = (from productVendor in data.ProductVendors
                                 where productVendor.Product.Name == productName
                                 select productVendor.Vendor.Name).FirstOrDefault();
                return result;
            }
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                Table<Product> products = data.GetTable<Product>();
                Table<ProductReview> reviewes = data.GetTable<ProductReview>();

                List<Product> result = (from product in products
                                        where product.ProductReviews.Count == howManyReviews
                                        select product).ToList();
                return result;
            }
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                Table<ProductReview> reviewes = data.GetTable<ProductReview>();
                List<Product> result = (from review in reviewes
                                        orderby review.ReviewDate descending
                                        group review.Product by review.ProductID into p
                                        select p.First()).Take(howManyProducts).ToList();
                return result;
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            DataClassesDataContext data = new DataClassesDataContext();
            Table<Product> products = data.GetTable<Product>();
            List<Product> result = (from product in products
                                    where product.ProductSubcategory.ProductCategory.Name == categoryName
                                    select product).Take(n).ToList();
            return result;
        }


        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (DataClassesDataContext data = new DataClassesDataContext())
            {
                Table<Product> products = data.GetTable<Product>();
                decimal result = (from product in products
                                  where (product.ProductSubcategory.ProductCategory.ProductCategoryID == category.ProductCategoryID || product.ProductSubcategory.ProductCategory.Name == category.Name)
                                  select product.StandardCost).ToList().Sum();
                return (int)result;
            }
        }    
    }
}





