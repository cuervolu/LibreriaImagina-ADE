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
    
    public partial class DETALLE_PEDIDO
    {
        public long ID_DETALLE_PEDIDO { get; set; }
        public long CANTIDAD { get; set; }
        public long PRECIO_UNITARIO { get; set; }
        public Nullable<long> SUBTOTAL { get; set; }
        public long LIBRO_ID { get; set; }
        public long PEDIDO_ID { get; set; }
    
        public virtual LIBRO LIBRO { get; set; }
        public virtual PEDIDO PEDIDO { get; set; }
    }
}
