using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.concrete;

namespace ADOSI2.dal
{
    public interface IContext : IDisposable
    {
        void Open();
        SqlCommand CreateCommand();
        void EnlistTransaction();

        ParqueRepository Parques { get; }
        EstadaRepository Estadas { get; }
    }
}
