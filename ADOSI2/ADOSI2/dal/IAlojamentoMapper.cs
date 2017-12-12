using ADOSI2.mapper;
using ADOSI2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.dal
{
    interface IAlojamentoMapper : IMapper<Alojamento, string, List<Alojamento>>
    {
    }
}
