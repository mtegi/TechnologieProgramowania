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

        public ProductDetailsViewModel(Object listModel) : this(listModel,new DataRepository()) { }
        public ProductDetailsViewModel(Object listModel, IProductService productService)
        {
            this.service = productService;
            ProductListModel selectedProductModel = (ProductListModel)listModel;
            this.product = service.GetDataForDetailsView(selectedProductModel.Id);
        }

    }
}
