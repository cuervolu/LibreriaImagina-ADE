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
    
    public partial class USUARIO_GROUPS
    {
        public long ID { get; set; }
        public long USUARIO_ID { get; set; }
        public long GROUP_ID { get; set; }
    
        public virtual AUTH_GROUP AUTH_GROUP { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
