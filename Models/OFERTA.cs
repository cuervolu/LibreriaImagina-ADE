//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaLibreriaImagina.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OFERTA
    {
        public long ID { get; set; }
        public decimal DESCUENTO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public long LIBRO_ID { get; set; }
    
        public virtual LIBRO LIBRO { get; set; }
    }
}
