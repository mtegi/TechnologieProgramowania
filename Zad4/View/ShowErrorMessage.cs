using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace View
{
    public class ShowErrorMessage
    {
        private MessageBoxResult Show;

        public ShowErrorMessage()
        {
            this.Show =  MessageBox.Show("Error");
        }

    }
}
