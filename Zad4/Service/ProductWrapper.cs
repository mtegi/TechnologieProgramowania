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

        public string Size
        {
            get
            {
                return product.Size;
            }
            set
            {
                product.Size = value;
            }
        }

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
                product.WeightUnitMeasureCode = value;
            }
        }



        
        public decimal? Weight
        {
            get
            {
                return product.Weight;
            }
            set
            {
                product.Weight = value;
            }
        }

        public int DaysToManufacture
        {
            get
            {
                return product.DaysToManufacture;
            }
            set
            {
                product.DaysToManufacture = value;
            }
        }
        public string ProductLine
        {
            get
            {
                return product.ProductLine;
            }
            set
            {
                product.ProductLine = value;
            }
        }



        public string Class {
            get
            {
                return product.ProductLine;
            }
            set
            {
                product.ProductLine = value;
            }
        }


        public string Style {
            get
            {
                return product.Style;
            }
            set
            {
                product.Style = value;
            }
                }

        public int? ProductSubcategoryID {
            get
            {
                return product.ProductSubcategoryID;
            }
            set
            {
                product.ProductSubcategoryID = value;
            }
}
        public int ModelId { get; set; }
        public DateTime? SellEndDate
        {
            get { return product.SellEndDate; }
            set { product.SellEndDate = value; }
        }
        public DateTime SellStartDate
        {
            get { return product.SellStartDate; }
            set { product.SellStartDate = value; }
        }
    }
}
