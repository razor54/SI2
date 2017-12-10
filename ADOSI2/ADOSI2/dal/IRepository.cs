using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.dal
{
    interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(System.Func<T, bool> criteria);
    }
}
