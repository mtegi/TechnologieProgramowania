using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace Model
{
    public class Model
    {
        public Model(IDataRepository data)
        {
            repository = data ?? throw new ArgumentNullException(nameof(data));
        }

        private IDataRepository repository;

    }
}
