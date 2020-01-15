using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public static class ProductFactory
    {

        public static ProductWrapper getNewWrapperProduct(string ProductName, string ProductNumber, bool MakeFlag, bool FinishedGoodsFlag,
            string Color, string SafetyStockLevel, string ReorderPoint, string StandardCost, 
            string ListPrice, string SizeUnitMeasureCode, string WeightUnitMeasureCode, string Weight, string DaysToManufacture, string ProductLine,
            string Class, string Style, string ProductSubcategoryID, string ModelId, DateTime SellStartDate, DateTime? SellEndDate)
        {

            Product product = new Product();
            product.Name = ProductName;


            product.ProductNumber = ProductNumber;
            product.MakeFlag = MakeFlag;
            product.FinishedGoodsFlag = FinishedGoodsFlag;
            product.Color = Color;
            product.SellStartDate = SellStartDate;
            product.SellEndDate = SellEndDate;

            short parse_result_short;
            decimal parse_result_decimal;
            Int32 parse_result_int;


            if (short.TryParse(SafetyStockLevel, out parse_result_short))
            product.SafetyStockLevel = parse_result_short;
            else
            product.SafetyStockLevel = 1;

            if (short.TryParse(ReorderPoint, out parse_result_short))
            product.ReorderPoint = parse_result_short;
            else
            product.ReorderPoint = 1;

            if (decimal.TryParse(StandardCost, out parse_result_decimal))
                product.StandardCost = parse_result_decimal;
            else
                product.StandardCost = 0;

            if (decimal.TryParse(ListPrice, out parse_result_decimal))
                product.ListPrice = parse_result_decimal;
            else
                product.ListPrice = 0;

            product.SizeUnitMeasureCode = SizeUnitMeasureCode;
            product.WeightUnitMeasureCode = WeightUnitMeasureCode;

            if (Decimal.TryParse(Weight, out parse_result_decimal))
                product.Weight = parse_result_decimal;
            else
                product.Weight = 1;

            if (Int32.TryParse(DaysToManufacture, out parse_result_int))
                product.DaysToManufacture = parse_result_int;
            else
                product.DaysToManufacture = 0;

            product.ProductLine = ProductLine;
            product.Class = Class;
            product.Style = Style;


            if (Int32.TryParse(ProductSubcategoryID, out parse_result_int))
                product.ProductSubcategoryID = parse_result_int;

            if (Int32.TryParse(ModelId, out parse_result_int))
                product.ProductModelID = parse_result_int;

            product.ModifiedDate = DateTime.Now;

            return new ProductWrapper(product);
        }
    }
}
