﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    /** Interfejs wypelniana kolekcji danymi */
   public interface IDataProvider
    {
        void Fill(DataContext data);
       
    }
}
