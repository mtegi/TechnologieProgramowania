﻿using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AddWindowViewModel
    {
        private readonly IProductService productService;
        public Command Add { get; private set; }

        public AddWindowViewModel() : this(new DataRepository())
        {
            this.Add = new Command(AddProduct);
        }

        public AddWindowViewModel(IProductService productService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        private void AddProduct()
        {
           // this.productService.Add();
        }
        
    }
}