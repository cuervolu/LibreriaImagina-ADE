using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SistemaLibreriaImagina.Models;
using SistemaLibreriaImagina.wsImaginaEnvio;
using SistemaLibreriaImagina.wsImaginaPay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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

        public static string GenerateShipment(DateTime fecha_envio, long direccion_id, long pedido_id, long repartidor_id)
        {
            ImaginaEnvioSoapClient cliente = new ImaginaEnvioSoapClient();
            try
            {
                if (IsServiceOnline(cliente.Endpoint.Address.Uri))
                {
                    var res = cliente.GenerateShipments(fecha_envio, direccion_id, pedido_id, repartidor_id);
                    return res;
                }
                else
                {
                    throw new Exception("El servicio de envío no está en línea.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear envío: {ex.Message}");
            }
        }

        public static async Task<string> CreatePayment(string user_rut, decimal total)
        {
            ImaginaPaySoapClient cliente = new ImaginaPaySoapClient();
            try
            {
                if (IsServiceOnline(cliente.Endpoint.Address.Uri))
                {
                    var response = await cliente.CreateBranchPaymentAsync(user_rut, total);
                    return response.ToString();
                }
                else
                {
                    throw new Exception("El servicio de pago no está en línea.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al hacer el pago: {ex.Message}");
            }
        }



        private static bool IsServiceOnline(Uri serviceUri)
        {
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    client.OpenRead(serviceUri);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
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