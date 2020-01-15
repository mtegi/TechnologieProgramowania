using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace  Model
{
    //Model produktów w lisce
    public class ProductListModel : INotifyPropertyChanged
    {
        
        private int _id;
        private string _name;
        public string Name {

            get
            {
                return _name; ;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Id { get { return _id; } set {
                _id= value;
                OnPropertyChanged("Id");
            } }

        public ProductListModel(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
