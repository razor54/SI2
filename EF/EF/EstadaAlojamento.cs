//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstadaAlojamento
    {
        public string nome_alojamento { get; set; }
        public decimal id_estada { get; set; }
        public decimal preço_base { get; set; }
        public string descrição { get; set; }
    
        public virtual Alojamento Alojamento { get; set; }
        public virtual Estada Estada { get; set; }
    }
}
