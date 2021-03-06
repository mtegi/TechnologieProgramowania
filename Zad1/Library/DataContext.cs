﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
   public class DataContext
    {

        public DataContext()
        {
            Readers = new List<Reader>();
            Books = new Dictionary<int, Book>(); ;
            Events = new ObservableCollection<LibEvent>();
            Copies = new Dictionary<int,Copy>();

        }

        public List<Reader> Readers { get; }
        public Dictionary<int, Book> Books { get; }
        public ObservableCollection<LibEvent> Events { get; }
        public Dictionary<int,Copy> Copies { get; }

    }
}
