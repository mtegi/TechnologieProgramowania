using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace Model 
{
    //Model widoku szczegółowego

    public class ProductModel : INotifyPropertyChanged  
    {
        //TODO: set property changed events

        private int _productID { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string Color { get; set; } = null;
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; } = null;
        public string SizeUnitMeasureCode { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public string ProductSubcategoryID { get; set; }
        public string ModelId { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime SellStartDate { get; set; }

        public ProductModel(int productID, string productName, string productNumber,
            bool makeFlag, bool finishedGoodsFlag, string color, short safetyStockLevel,
            short reorderPoint, decimal standardCost, decimal listPrice, string size,
            string sizeUnitMeasureCode, string weightUnitMeasureCode, decimal? weight,
            int daysToManufacture, string productLine, string @class, string style,
            string productSubcategoryID, string modelId, DateTime? sellEndDate,
            DateTime sellStartDate)
        {
            _productID = productID;
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            ProductNumber = productNumber ?? throw new ArgumentNullException(nameof(productNumber));
            MakeFlag = makeFlag;
            FinishedGoodsFlag = finishedGoodsFlag;
            Color = color ?? throw new ArgumentNullException(nameof(color));
            SafetyStockLevel = safetyStockLevel;
            ReorderPoint = reorderPoint;
            StandardCost = standardCost;
            ListPrice = listPrice;
            Size = size ?? throw new ArgumentNullException(nameof(size));
            SizeUnitMeasureCode = sizeUnitMeasureCode ?? throw new ArgumentNullException(nameof(sizeUnitMeasureCode));
            WeightUnitMeasureCode = weightUnitMeasureCode ?? throw new ArgumentNullException(nameof(weightUnitMeasureCode));
            Weight = weight ?? throw new ArgumentNullException(nameof(weight));
            DaysToManufacture = daysToManufacture;
            ProductLine = productLine ?? throw new ArgumentNullException(nameof(productLine));
            Class = @class ?? throw new ArgumentNullException(nameof(@class));
            Style = style ?? throw new ArgumentNullException(nameof(style));
            ProductSubcategoryID = productSubcategoryID ?? throw new ArgumentNullException(nameof(productSubcategoryID));
            ModelId = modelId ?? throw new ArgumentNullException(nameof(modelId));
            SellEndDate = sellEndDate ?? throw new ArgumentNullException(nameof(sellEndDate));
            SellStartDate = sellStartDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
