using Oracle.ManagedDataAccess.Client;
using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public static class BookService
{
    public static List<LIBRO> GetBookList(int startIndex, int pageSize)
    {
        Entities dbContext = null;

        try
        {
            dbContext = new Entities();

            var pCursor = new OracleParameter
            {
                ParameterName = "p_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };

            var pStartIndex = new OracleParameter
            {
                ParameterName = "p_start_index",
                OracleDbType = OracleDbType.Int32,
                Value = startIndex
            };

            var pPageSize = new OracleParameter
            {
                ParameterName = "p_page_size",
                OracleDbType = OracleDbType.Int32,
                Value = pageSize
            };

            var libros = dbContext.Database.SqlQuery<LIBRO>("BEGIN listar_libros(:p_cursor, :p_start_index, :p_page_size); END;", pCursor, pStartIndex, pPageSize).ToList();

            return libros;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la lista de libros: {ex.Message}");
            return null;
        }
        finally
        {
            // Cerrar la conexión en el bloque finally
            dbContext?.Dispose();
        }
    }
}
