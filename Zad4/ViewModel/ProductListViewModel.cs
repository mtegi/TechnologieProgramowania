using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class ProductListViewModel : INotifyPropertyChanged
    {
        //Lista produktow - widok produktow jako lista
        public ObservableCollection<ProductListModel> ProductsInList { get; set; }
        public Object SelectedProduct{ get; set; }

        private readonly IProductService productService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductListViewModel():this(new DataRepository()){}
        public ProductListViewModel(IProductService productService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            ProductsInList = new ObservableCollection<ProductListModel>();
            Fill(productService.GetDataForListView());
        }


        private void Fill(IEnumerable<Tuple<int,string>> data)    
        {   
            ProductsInList.Clear();
            foreach(Tuple<int, string> item in data)
            {
                ProductsInList.Add(new ProductListModel(item.Item1, item.Item2));
            }
        }

        private void OpenDetilsView()
        {

        }
    }
}   
