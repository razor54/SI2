﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSI2.model
{
    class Bungalow
    {
        public string Tipologia { get; set; }
        public virtual Alojamento NomeAlojamento { get; set; }
    }
}
