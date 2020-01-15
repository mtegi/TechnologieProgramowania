using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   
   public class ProductDetailsViewModel
    {
        private ProductWrapper product;
        private IProductService service;
        public Command saveDetailsCommand { get; private set; }



        public ProductDetailsViewModel(Object listModel) : this(listModel,new DataRepository()) { }
        public ProductDetailsViewModel(Object listModel, IProductService productService)
        {
            this.service = productService;
            ProductListModel selectedProductModel = (ProductListModel)listModel;
            this.product = service.GetDataForDetailsView(selectedProductModel.Id);
            this.saveDetailsCommand = new Command (saveDetails);

            ProductName = product.ProductName;
            ProductNumber = product.ProductNumber;


        }

        private void saveDetails()
        {

        }

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



    }
}
