using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOSI2.model;

namespace ADOSI2.mapper
{
    interface IEstadaAlojamentoMapper : IMapper<EstadaAlojamento, KeyValuePair<string, int>, List<EstadaAlojamento>>
    {
    }
}
