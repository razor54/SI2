using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.dal
{
    interface IContext : IDisposable
    {
        void Open();
        SqlCommand CreateCommand();
        void EnlistTransaction();
    }
}
