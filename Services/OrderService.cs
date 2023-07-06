using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
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

        public static int GetAmountOrders()
        {

            try
            {
                using (var dbContext = new Entities())
                {
                    var cantidadParam = new OracleParameter
                    {
                        ParameterName = "cantidad",
                        Direction = ParameterDirection.Output,
                        OracleDbType = OracleDbType.Int32
                    };

                    dbContext.Database.ExecuteSqlCommand("BEGIN MostrarCantidadPedidos (:cantidad); END;", cantidadParam);

                    var cantidadPedidos = 0;

                    if (cantidadParam.Value is OracleDecimal oracleDecimal)
                    {
                        try
                        {
                            cantidadPedidos = oracleDecimal.ToInt32();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al convertir el valor del parámetro a int: " + ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("El valor del parámetro no es un OracleDecimal.");
                    }

                    return cantidadPedidos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad de pedidos: " + ex.Message);
                return 0;
            }
        }


        public static (double[] Values, string[] Labels) GetOrderStatusCount()
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    var statusCounts = dbContext.PEDIDOes
                        .GroupBy(p => p.ESTADO_PEDIDO)
                        .ToDictionary(g => g.Key, g => g.Count());

                    double[] values = statusCounts.Values.Select(v => (double)v).ToArray();
                    string[] labels = statusCounts.Keys.ToArray();

                    return (values, labels);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el número de pedidos por estado: {ex.Message}");
                return (null, null);
            }
        }





    }
}