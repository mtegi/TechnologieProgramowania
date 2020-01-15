using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
   
   public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private int productID;
        ProductListModel selectedProductModel;
        private ProductWrapper product;
        private IProductService service;
        public event PropertyChangedEventHandler PropertyChanged;
        public Command SaveDetailsCommand { get; private set; }



        public ProductDetailsViewModel(Object listModel) : this(listModel,new DataRepository()) { }
        public ProductDetailsViewModel(Object listModel, IProductService productService)
        {
            this.service = productService;
            selectedProductModel = (ProductListModel)listModel;
            productID = selectedProductModel.Id;
            this.product = service.GetDataForDetailsView(productID);
            this.SaveDetailsCommand = new Command (SaveDetails);
            ProductName = product.ProductName;
            ProductNumber = product.ProductNumber;
            MakeFlag = product.MakeFlag;
            FinishedGoodsFlag = product.FinishedGoodsFlag;
            Color = product.Color;
            SafetyStockLevel = product.SafetyStockLevel.ToString(); //short
            ReorderPoint = product.ReorderPoint.ToString(); //short
            StandardCost = product.StandardCost.ToString(); //decimal
            ListPrice = product.ListPrice.ToString(); //decimal
            SizeUnitMeasureCode = product.SizeUnitMeasureCode;
            WeightUnitMeasureCode = product.WeightUnitMeasureCode;


        }




        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public string SafetyStockLevel { get; set; }
        public string ReorderPoint { get; set; }
        public string StandardCost { get; set; }
        public string ListPrice { get; set; }
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


        private void SaveDetails()
        {
            product.ProductName = ProductName;
            product.ProductNumber = ProductNumber;
            product.MakeFlag = MakeFlag;
            product.FinishedGoodsFlag = FinishedGoodsFlag;
            product.Color = Color;

            service.Update(product);

            this.product = service.GetDataForDetailsView(productID);
            selectedProductModel.Name = this.product.ProductName;

            product.ToString();
        }


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
