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
    
    public partial class MANTENIMIENTO
    {
        public long ID_MANTENIMIENTO { get; set; }
        public System.DateTime FECHA_SOLICITUD { get; set; }
        public string TIPO_MANTENIMIENTO { get; set; }
        public string ESTADO_MANTENIMIENTO { get; set; }
        public long CLIENTE_ID { get; set; }
        public long LIBRO_ID { get; set; }
        public long TECNICO_ID { get; set; }
        public string COMENTARIO { get; set; }
        public string RESPUESTA { get; set; }
    
        public virtual LIBRO LIBRO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
    }
}
