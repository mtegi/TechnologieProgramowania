using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductWrapper
    {
        private Product product;


        public ProductWrapper(Product product)
        {
            this.product = product;
        }


        internal Product getProduct()
        {
            return this.product;
        }

        public int ProductID
        {
            get
            {
               return product.ProductID;
            }
        }

        public string ProductName {
            get
            {
                return product.Name;
            }
            set
            {
                product.Name = value;
            }
        }

        public string ProductNumber {
            get
            {
                return product.ProductNumber;
            }
            set
            {
                product.ProductNumber = value;
            }
        }

        public bool MakeFlag
        {
            get
            {
                return product.MakeFlag;

            }
            set
            {
                product.MakeFlag = value;
            }
        }

        public bool FinishedGoodsFlag {
            get
            {
                return product.FinishedGoodsFlag;
            }
            set
            {
                product.FinishedGoodsFlag = value;
            }
                
         }

        public string Color
        {
            get
            {
                return product.Color;
            }
            set
            {
                product.Color = value;
            }
        }

        public short SafetyStockLevel
        {
            get
            {
                return product.SafetyStockLevel;
            }
            set
            {
                product.SafetyStockLevel = value;
            }
        }
        public short ReorderPoint
        {
            get
            {
                return product.ReorderPoint;
            }
            set
            {
                product.ReorderPoint = value;
            }
        }
        public decimal StandardCost
        {
            get
            {
                return product.StandardCost;
            }
            set
            {
                product.StandardCost = value;
            }
        }
        public decimal ListPrice
        {
            get
            {
                return product.ListPrice;
            }
            set
            {
                product.ListPrice = value;
            }
        }

     //   public string Size { get; set; } = null;

        public string SizeUnitMeasureCode {
            get
            {
                return product.SizeUnitMeasureCode;
            }
            set
            {
                product.SizeUnitMeasureCode = value;
            }
        }

        public string WeightUnitMeasureCode
        {
            get
            {
                return product.WeightUnitMeasureCode;
            }
            set
            {
                this.WeightUnitMeasureCode = value;
            }
        }



        
        public decimal? Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public string ProductSubcategoryID { get; set; }
        public string ModelId { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime SellStartDate { get; set; }

    }
}
