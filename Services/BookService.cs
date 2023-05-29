using Oracle.ManagedDataAccess.Client;
using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public static class BookService
{
    public static List<LIBRO> GetBookList()
    {
        try
        {
            using (var dbContext = new Entities())
            {
                // Crea un parámetro de salida para el cursor del procedimiento almacenado
                var pCursor = new OracleParameter
                {
                    ParameterName = "p_cursor",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor
                };

                // Llama al procedimiento almacenado utilizando SqlQuery
                var libros = dbContext.Database.SqlQuery<LIBRO>("BEGIN listar_libros(:p_cursor); END;", pCursor).ToList();

                return libros;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la lista de libros: {ex.Message}");
            return null;
        }
    }
}
