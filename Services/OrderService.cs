using Oracle.ManagedDataAccess.Client;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.wsImaginaEnvio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

namespace SistemaLibreriaImagina.Services
{
    public static class OrderService
    {
        public static List<PEDIDO> GetOrdersList()
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                    cursorParam.Direction = ParameterDirection.Output;

                    var pedidos = dbContext.Database.SqlQuery<PEDIDO>("BEGIN LISTAR_PEDIDOS(:p_cursor); END;", cursorParam).ToList();

                    return pedidos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de pedidos: {ex.Message}");
                return null;
            }
        }

        public static PEDIDO GetPEDIDO(long id_pedido)
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    PEDIDO pedido = dbContext.PEDIDOes.FirstOrDefault(p => p.ID_PEDIDO == id_pedido);
                    return pedido;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static XmlDocument GenerateShipment(DateTime fecha_envio, long direccion_id, long pedido_id, long repartidor_id)
        {
            ImaginaEnvioSoapClient cliente = new ImaginaEnvioSoapClient();
            try
            {
                var respuestaXml = cliente.GenerateShipments(fecha_envio, direccion_id, pedido_id, repartidor_id);

                if (!string.IsNullOrEmpty(respuestaXml))
                {
                    // Crear un objeto XmlDocument y cargar la respuesta XML en él
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(respuestaXml);

                    return xmlDoc;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null; // En caso de que no haya respuesta o haya ocurrido un error
        }
    }
}