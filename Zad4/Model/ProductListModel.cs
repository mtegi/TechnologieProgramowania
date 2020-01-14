using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Model
{
    //Model produktów w lisce
    public class ProductListModel
    {
        public int Id { get; }
        public string Name { get; }

        public ProductListModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
