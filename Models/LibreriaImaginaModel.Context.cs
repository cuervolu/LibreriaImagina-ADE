﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AUTH_GROUP> AUTH_GROUP { get; set; }
        public virtual DbSet<AUTH_GROUP_PERMISSIONS> AUTH_GROUP_PERMISSIONS { get; set; }
        public virtual DbSet<AUTH_PERMISSION> AUTH_PERMISSION { get; set; }
        public virtual DbSet<AUTHTOKEN_TOKEN> AUTHTOKEN_TOKEN { get; set; }
        public virtual DbSet<CARRITO> CARRITOes { get; set; }
        public virtual DbSet<COMUNA> COMUNAs { get; set; }
        public virtual DbSet<DETALLE_CARRITO> DETALLE_CARRITO { get; set; }
        public virtual DbSet<DETALLE_PEDIDO> DETALLE_PEDIDO { get; set; }
        public virtual DbSet<DIRECCION> DIRECCIONs { get; set; }
        public virtual DbSet<DJANGO_ADMIN_LOG> DJANGO_ADMIN_LOG { get; set; }
        public virtual DbSet<DJANGO_CONTENT_TYPE> DJANGO_CONTENT_TYPE { get; set; }
        public virtual DbSet<DJANGO_MIGRATIONS> DJANGO_MIGRATIONS { get; set; }
        public virtual DbSet<DJANGO_SESSION> DJANGO_SESSION { get; set; }
        public virtual DbSet<ENVIO> ENVIOs { get; set; }
        public virtual DbSet<LIBRO> LIBROes { get; set; }
        public virtual DbSet<MANTENIMIENTO> MANTENIMIENTOes { get; set; }
        public virtual DbSet<METODO_PAGO> METODO_PAGO { get; set; }
        public virtual DbSet<OFERTA> OFERTAs { get; set; }
        public virtual DbSet<PEDIDO> PEDIDOes { get; set; }
        public virtual DbSet<REGION> REGIONs { get; set; }
        public virtual DbSet<TRANSACCION> TRANSACCIONs { get; set; }
        public virtual DbSet<USUARIO> USUARIOs { get; set; }
        public virtual DbSet<USUARIO_GROUPS> USUARIO_GROUPS { get; set; }
        public virtual DbSet<USUARIO_USER_PERMISSIONS> USUARIO_USER_PERMISSIONS { get; set; }
    
        public virtual int ACTUALIZAR_MANTENIMIENTO(Nullable<decimal> p_ID_MANTENIMIENTO, Nullable<System.DateTime> p_FECHA_SOLICITUD, string p_TIPO_MANTENIMIENTO, string p_ESTADO_MANTENIMIENTO, Nullable<decimal> p_CLIENTE_ID, Nullable<decimal> p_LIBRO_ID, Nullable<decimal> p_TECNICO_ID, string p_COMENTARIO)
        {
            var p_ID_MANTENIMIENTOParameter = p_ID_MANTENIMIENTO.HasValue ?
                new ObjectParameter("P_ID_MANTENIMIENTO", p_ID_MANTENIMIENTO) :
                new ObjectParameter("P_ID_MANTENIMIENTO", typeof(decimal));
    
            var p_FECHA_SOLICITUDParameter = p_FECHA_SOLICITUD.HasValue ?
                new ObjectParameter("P_FECHA_SOLICITUD", p_FECHA_SOLICITUD) :
                new ObjectParameter("P_FECHA_SOLICITUD", typeof(System.DateTime));
    
            var p_TIPO_MANTENIMIENTOParameter = p_TIPO_MANTENIMIENTO != null ?
                new ObjectParameter("P_TIPO_MANTENIMIENTO", p_TIPO_MANTENIMIENTO) :
                new ObjectParameter("P_TIPO_MANTENIMIENTO", typeof(string));
    
            var p_ESTADO_MANTENIMIENTOParameter = p_ESTADO_MANTENIMIENTO != null ?
                new ObjectParameter("P_ESTADO_MANTENIMIENTO", p_ESTADO_MANTENIMIENTO) :
                new ObjectParameter("P_ESTADO_MANTENIMIENTO", typeof(string));
    
            var p_CLIENTE_IDParameter = p_CLIENTE_ID.HasValue ?
                new ObjectParameter("P_CLIENTE_ID", p_CLIENTE_ID) :
                new ObjectParameter("P_CLIENTE_ID", typeof(decimal));
    
            var p_LIBRO_IDParameter = p_LIBRO_ID.HasValue ?
                new ObjectParameter("P_LIBRO_ID", p_LIBRO_ID) :
                new ObjectParameter("P_LIBRO_ID", typeof(decimal));
    
            var p_TECNICO_IDParameter = p_TECNICO_ID.HasValue ?
                new ObjectParameter("P_TECNICO_ID", p_TECNICO_ID) :
                new ObjectParameter("P_TECNICO_ID", typeof(decimal));
    
            var p_COMENTARIOParameter = p_COMENTARIO != null ?
                new ObjectParameter("P_COMENTARIO", p_COMENTARIO) :
                new ObjectParameter("P_COMENTARIO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ACTUALIZAR_MANTENIMIENTO", p_ID_MANTENIMIENTOParameter, p_FECHA_SOLICITUDParameter, p_TIPO_MANTENIMIENTOParameter, p_ESTADO_MANTENIMIENTOParameter, p_CLIENTE_IDParameter, p_LIBRO_IDParameter, p_TECNICO_IDParameter, p_COMENTARIOParameter);
        }
    
        public virtual int AGREGAR_LIBRO(string p_NOMBRE_LIBRO, string p_DESCRIPCION, string p_AUTOR, string p_EDITORIAL, Nullable<decimal> p_PRECIO_UNITARIO, Nullable<decimal> p_CANTIDAD_DISPONIBLE, string p_PORTADA, Nullable<System.DateTime> p_FECHA_PUBLICACION, string p_CATEGORIA, string p_ISBN, string p_SLUG, string p_THUMBNAIL)
        {
            var p_NOMBRE_LIBROParameter = p_NOMBRE_LIBRO != null ?
                new ObjectParameter("P_NOMBRE_LIBRO", p_NOMBRE_LIBRO) :
                new ObjectParameter("P_NOMBRE_LIBRO", typeof(string));
    
            var p_DESCRIPCIONParameter = p_DESCRIPCION != null ?
                new ObjectParameter("P_DESCRIPCION", p_DESCRIPCION) :
                new ObjectParameter("P_DESCRIPCION", typeof(string));
    
            var p_AUTORParameter = p_AUTOR != null ?
                new ObjectParameter("P_AUTOR", p_AUTOR) :
                new ObjectParameter("P_AUTOR", typeof(string));
    
            var p_EDITORIALParameter = p_EDITORIAL != null ?
                new ObjectParameter("P_EDITORIAL", p_EDITORIAL) :
                new ObjectParameter("P_EDITORIAL", typeof(string));
    
            var p_PRECIO_UNITARIOParameter = p_PRECIO_UNITARIO.HasValue ?
                new ObjectParameter("P_PRECIO_UNITARIO", p_PRECIO_UNITARIO) :
                new ObjectParameter("P_PRECIO_UNITARIO", typeof(decimal));
    
            var p_CANTIDAD_DISPONIBLEParameter = p_CANTIDAD_DISPONIBLE.HasValue ?
                new ObjectParameter("P_CANTIDAD_DISPONIBLE", p_CANTIDAD_DISPONIBLE) :
                new ObjectParameter("P_CANTIDAD_DISPONIBLE", typeof(decimal));
    
            var p_PORTADAParameter = p_PORTADA != null ?
                new ObjectParameter("P_PORTADA", p_PORTADA) :
                new ObjectParameter("P_PORTADA", typeof(string));
    
            var p_FECHA_PUBLICACIONParameter = p_FECHA_PUBLICACION.HasValue ?
                new ObjectParameter("P_FECHA_PUBLICACION", p_FECHA_PUBLICACION) :
                new ObjectParameter("P_FECHA_PUBLICACION", typeof(System.DateTime));
    
            var p_CATEGORIAParameter = p_CATEGORIA != null ?
                new ObjectParameter("P_CATEGORIA", p_CATEGORIA) :
                new ObjectParameter("P_CATEGORIA", typeof(string));
    
            var p_ISBNParameter = p_ISBN != null ?
                new ObjectParameter("P_ISBN", p_ISBN) :
                new ObjectParameter("P_ISBN", typeof(string));
    
            var p_SLUGParameter = p_SLUG != null ?
                new ObjectParameter("P_SLUG", p_SLUG) :
                new ObjectParameter("P_SLUG", typeof(string));
    
            var p_THUMBNAILParameter = p_THUMBNAIL != null ?
                new ObjectParameter("P_THUMBNAIL", p_THUMBNAIL) :
                new ObjectParameter("P_THUMBNAIL", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AGREGAR_LIBRO", p_NOMBRE_LIBROParameter, p_DESCRIPCIONParameter, p_AUTORParameter, p_EDITORIALParameter, p_PRECIO_UNITARIOParameter, p_CANTIDAD_DISPONIBLEParameter, p_PORTADAParameter, p_FECHA_PUBLICACIONParameter, p_CATEGORIAParameter, p_ISBNParameter, p_SLUGParameter, p_THUMBNAILParameter);
        }
    
        public virtual int CREAR_MANTENIMIENTO(Nullable<System.DateTime> p_FECHA_SOLICITUD, string p_TIPO_MANTENIMIENTO, string p_ESTADO_MANTENIMIENTO, Nullable<decimal> p_CLIENTE_ID, Nullable<decimal> p_LIBRO_ID, Nullable<decimal> p_TECNICO_ID, string p_COMENTARIO)
        {
            var p_FECHA_SOLICITUDParameter = p_FECHA_SOLICITUD.HasValue ?
                new ObjectParameter("P_FECHA_SOLICITUD", p_FECHA_SOLICITUD) :
                new ObjectParameter("P_FECHA_SOLICITUD", typeof(System.DateTime));
    
            var p_TIPO_MANTENIMIENTOParameter = p_TIPO_MANTENIMIENTO != null ?
                new ObjectParameter("P_TIPO_MANTENIMIENTO", p_TIPO_MANTENIMIENTO) :
                new ObjectParameter("P_TIPO_MANTENIMIENTO", typeof(string));
    
            var p_ESTADO_MANTENIMIENTOParameter = p_ESTADO_MANTENIMIENTO != null ?
                new ObjectParameter("P_ESTADO_MANTENIMIENTO", p_ESTADO_MANTENIMIENTO) :
                new ObjectParameter("P_ESTADO_MANTENIMIENTO", typeof(string));
    
            var p_CLIENTE_IDParameter = p_CLIENTE_ID.HasValue ?
                new ObjectParameter("P_CLIENTE_ID", p_CLIENTE_ID) :
                new ObjectParameter("P_CLIENTE_ID", typeof(decimal));
    
            var p_LIBRO_IDParameter = p_LIBRO_ID.HasValue ?
                new ObjectParameter("P_LIBRO_ID", p_LIBRO_ID) :
                new ObjectParameter("P_LIBRO_ID", typeof(decimal));
    
            var p_TECNICO_IDParameter = p_TECNICO_ID.HasValue ?
                new ObjectParameter("P_TECNICO_ID", p_TECNICO_ID) :
                new ObjectParameter("P_TECNICO_ID", typeof(decimal));
    
            var p_COMENTARIOParameter = p_COMENTARIO != null ?
                new ObjectParameter("P_COMENTARIO", p_COMENTARIO) :
                new ObjectParameter("P_COMENTARIO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CREAR_MANTENIMIENTO", p_FECHA_SOLICITUDParameter, p_TIPO_MANTENIMIENTOParameter, p_ESTADO_MANTENIMIENTOParameter, p_CLIENTE_IDParameter, p_LIBRO_IDParameter, p_TECNICO_IDParameter, p_COMENTARIOParameter);
        }
    
        public virtual int ELIMINAR_LIBRO(Nullable<decimal> p_ID_LIBRO)
        {
            var p_ID_LIBROParameter = p_ID_LIBRO.HasValue ?
                new ObjectParameter("P_ID_LIBRO", p_ID_LIBRO) :
                new ObjectParameter("P_ID_LIBRO", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ELIMINAR_LIBRO", p_ID_LIBROParameter);
        }
    
        public virtual int ELIMINAR_MANTENIMIENTO(Nullable<decimal> p_ID_MANTENIMIENTO)
        {
            var p_ID_MANTENIMIENTOParameter = p_ID_MANTENIMIENTO.HasValue ?
                new ObjectParameter("P_ID_MANTENIMIENTO", p_ID_MANTENIMIENTO) :
                new ObjectParameter("P_ID_MANTENIMIENTO", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ELIMINAR_MANTENIMIENTO", p_ID_MANTENIMIENTOParameter);
        }
    
        public virtual int LEER_MANTENIMIENTO(Nullable<decimal> p_ID_MANTENIMIENTO, ObjectParameter p_FECHA_SOLICITUD, ObjectParameter p_TIPO_MANTENIMIENTO, ObjectParameter p_ESTADO_MANTENIMIENTO, ObjectParameter p_CLIENTE_ID, ObjectParameter p_LIBRO_ID, ObjectParameter p_TECNICO_ID, ObjectParameter p_COMENTARIO)
        {
            var p_ID_MANTENIMIENTOParameter = p_ID_MANTENIMIENTO.HasValue ?
                new ObjectParameter("P_ID_MANTENIMIENTO", p_ID_MANTENIMIENTO) :
                new ObjectParameter("P_ID_MANTENIMIENTO", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LEER_MANTENIMIENTO", p_ID_MANTENIMIENTOParameter, p_FECHA_SOLICITUD, p_TIPO_MANTENIMIENTO, p_ESTADO_MANTENIMIENTO, p_CLIENTE_ID, p_LIBRO_ID, p_TECNICO_ID, p_COMENTARIO);
        }
    
        public virtual int LISTAR_CATEGORIAS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LISTAR_CATEGORIAS");
        }
    
        public virtual int LISTAR_LIBROS(Nullable<decimal> p_START_INDEX, Nullable<decimal> p_PAGE_SIZE)
        {
            var p_START_INDEXParameter = p_START_INDEX.HasValue ?
                new ObjectParameter("P_START_INDEX", p_START_INDEX) :
                new ObjectParameter("P_START_INDEX", typeof(decimal));
    
            var p_PAGE_SIZEParameter = p_PAGE_SIZE.HasValue ?
                new ObjectParameter("P_PAGE_SIZE", p_PAGE_SIZE) :
                new ObjectParameter("P_PAGE_SIZE", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LISTAR_LIBROS", p_START_INDEXParameter, p_PAGE_SIZEParameter);
        }
    
        public virtual int MODIFICAR_LIBRO(Nullable<decimal> p_ID_LIBRO, string p_NOMBRE_LIBRO, string p_DESCRIPCION, string p_AUTOR, string p_EDITORIAL, Nullable<decimal> p_PRECIO_UNITARIO, Nullable<decimal> p_CANTIDAD_DISPONIBLE, string p_PORTADA, Nullable<System.DateTime> p_FECHA_PUBLICACION, string p_CATEGORIA, string p_ISBN, string p_SLUG, string p_THUMBNAIL, ObjectParameter p_RESULTADO)
        {
            var p_ID_LIBROParameter = p_ID_LIBRO.HasValue ?
                new ObjectParameter("P_ID_LIBRO", p_ID_LIBRO) :
                new ObjectParameter("P_ID_LIBRO", typeof(decimal));
    
            var p_NOMBRE_LIBROParameter = p_NOMBRE_LIBRO != null ?
                new ObjectParameter("P_NOMBRE_LIBRO", p_NOMBRE_LIBRO) :
                new ObjectParameter("P_NOMBRE_LIBRO", typeof(string));
    
            var p_DESCRIPCIONParameter = p_DESCRIPCION != null ?
                new ObjectParameter("P_DESCRIPCION", p_DESCRIPCION) :
                new ObjectParameter("P_DESCRIPCION", typeof(string));
    
            var p_AUTORParameter = p_AUTOR != null ?
                new ObjectParameter("P_AUTOR", p_AUTOR) :
                new ObjectParameter("P_AUTOR", typeof(string));
    
            var p_EDITORIALParameter = p_EDITORIAL != null ?
                new ObjectParameter("P_EDITORIAL", p_EDITORIAL) :
                new ObjectParameter("P_EDITORIAL", typeof(string));
    
            var p_PRECIO_UNITARIOParameter = p_PRECIO_UNITARIO.HasValue ?
                new ObjectParameter("P_PRECIO_UNITARIO", p_PRECIO_UNITARIO) :
                new ObjectParameter("P_PRECIO_UNITARIO", typeof(decimal));
    
            var p_CANTIDAD_DISPONIBLEParameter = p_CANTIDAD_DISPONIBLE.HasValue ?
                new ObjectParameter("P_CANTIDAD_DISPONIBLE", p_CANTIDAD_DISPONIBLE) :
                new ObjectParameter("P_CANTIDAD_DISPONIBLE", typeof(decimal));
    
            var p_PORTADAParameter = p_PORTADA != null ?
                new ObjectParameter("P_PORTADA", p_PORTADA) :
                new ObjectParameter("P_PORTADA", typeof(string));
    
            var p_FECHA_PUBLICACIONParameter = p_FECHA_PUBLICACION.HasValue ?
                new ObjectParameter("P_FECHA_PUBLICACION", p_FECHA_PUBLICACION) :
                new ObjectParameter("P_FECHA_PUBLICACION", typeof(System.DateTime));
    
            var p_CATEGORIAParameter = p_CATEGORIA != null ?
                new ObjectParameter("P_CATEGORIA", p_CATEGORIA) :
                new ObjectParameter("P_CATEGORIA", typeof(string));
    
            var p_ISBNParameter = p_ISBN != null ?
                new ObjectParameter("P_ISBN", p_ISBN) :
                new ObjectParameter("P_ISBN", typeof(string));
    
            var p_SLUGParameter = p_SLUG != null ?
                new ObjectParameter("P_SLUG", p_SLUG) :
                new ObjectParameter("P_SLUG", typeof(string));
    
            var p_THUMBNAILParameter = p_THUMBNAIL != null ?
                new ObjectParameter("P_THUMBNAIL", p_THUMBNAIL) :
                new ObjectParameter("P_THUMBNAIL", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MODIFICAR_LIBRO", p_ID_LIBROParameter, p_NOMBRE_LIBROParameter, p_DESCRIPCIONParameter, p_AUTORParameter, p_EDITORIALParameter, p_PRECIO_UNITARIOParameter, p_CANTIDAD_DISPONIBLEParameter, p_PORTADAParameter, p_FECHA_PUBLICACIONParameter, p_CATEGORIAParameter, p_ISBNParameter, p_SLUGParameter, p_THUMBNAILParameter, p_RESULTADO);
        }
    
        public virtual int MOSTRARCANTIDADLIBROS(ObjectParameter cANTIDAD)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MOSTRARCANTIDADLIBROS", cANTIDAD);
        }
    
        public virtual int OBTENERLIBROPORID(Nullable<decimal> p_IDLIBRO)
        {
            var p_IDLIBROParameter = p_IDLIBRO.HasValue ?
                new ObjectParameter("P_IDLIBRO", p_IDLIBRO) :
                new ObjectParameter("P_IDLIBRO", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OBTENERLIBROPORID", p_IDLIBROParameter);
        }
    
        public virtual int LISTAR_PEDIDOS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LISTAR_PEDIDOS");
        }
    }
}
