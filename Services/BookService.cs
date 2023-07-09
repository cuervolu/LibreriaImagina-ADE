using dotenv.net;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public static class BookService
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
    }

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

    public static async Task<ApiResponse<List<LIBRO>>> GetLibrosFromAPI(string category)
    {
        DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: false));
        var envVars = DotEnv.Read();

        HttpClient httpClient = null;
        var apiResponse = new ApiResponse<List<LIBRO>>();

        try
        {
            // 1. Crear una instancia del cliente HTTP
            httpClient = new HttpClient();

            // 2. Realizar la solicitud HTTP al servidor de la API
            var response = await httpClient.GetAsync($"{envVars["API_URL"]}api/libros/get_libros_by_categoria/{category}/");

            // 3. Verificar si la respuesta fue exitosa
            response.EnsureSuccessStatusCode();

            // 4. Leer el contenido de la respuesta
            var content = await response.Content.ReadAsStringAsync();

            // 5. Deserializar el contenido utilizando Newtonsoft.Json
            var data = JsonConvert.DeserializeObject<List<LIBRO>>(content);

            apiResponse.Data = data; // Asignar los datos al objeto ApiResponse

            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            // Manejar la excepción de la solicitud HTTP
            apiResponse.Error = $"Error al obtener los libros de la API: {ex.Message}";
        }
        catch (JsonException ex)
        {
            // Manejar la excepción de deserialización JSON
            apiResponse.Error = $"Error al deserializar los datos de la API: {ex.Message}";
        }
        catch (Exception ex)
        {
            // Manejar cualquier otra excepción
            apiResponse.Error = $"Error en la solicitud a la API: {ex.Message}";
        }
        finally
        {
            // 6. Realizar tareas de limpieza o liberación de recursos
            httpClient?.Dispose();
        }

        return apiResponse;
    }

    public static async Task<List<string>> GetCategoryListAsync()
    {
        using (Entities dbContext = new Entities())
        {
            var pCursor = new OracleParameter
            {
                ParameterName = "p_cursor",
                Direction = ParameterDirection.Output,
                OracleDbType = OracleDbType.RefCursor
            };

            var query = "BEGIN listar_categorias(:p_cursor); END;";
            var categorias = await dbContext.Database.SqlQuery<string>(query, pCursor).ToListAsync();

            return categorias;
        }
    }

    public static void EliminarLibro(long idLibro)
    {
        Entities dbContext = null;

        try
        {
            dbContext = new Entities();

            var pIdLibro = new OracleParameter
            {
                ParameterName = "p_id_libro",
                OracleDbType = OracleDbType.Int64,
                Value = idLibro
            };

            var query = "BEGIN eliminar_libro(:p_id_libro); END;";
            dbContext.Database.ExecuteSqlCommand(query, pIdLibro);

            Console.WriteLine("Libro eliminado correctamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar el libro: {ex.Message}");
        }
        finally
        {
            dbContext?.Dispose();
        }
    }

    public static async Task<object> ObtenerLibroPorId(long idLibro)
    {
        try
        {
            using (Entities dbContext = new Entities())
            {
                var idLibroParameter = new OracleParameter
                {
                    ParameterName = "p_IdLibro",
                    OracleDbType = OracleDbType.Int64,
                    Direction = ParameterDirection.Input,
                    Value = idLibro
                };
                var cursorParameter = new OracleParameter
                {
                    ParameterName = "p_cursor",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor,
                };

                var libro = await dbContext.Database.SqlQuery<LIBRO>("BEGIN ObtenerLibroPorId(:p_IdLibro, :p_Cursor); END;", idLibroParameter, cursorParameter).FirstOrDefaultAsync();

                if (libro == null)
                {
                    return "El libro con ID " + idLibro + " no existe.";
                }

                return libro;
            }
        }
        catch (Exception ex)
        {
            return "Error al obtener el libro por ID: " + ex.Message;
        }
    }

    public static void ModificarLibro(
        long p_id_libro,
        string p_nombre_libro,
        string p_descripcion,
        string p_autor,
        string p_editorial,
        decimal p_precio_unitario,
        long p_cantidad_disponible,
        string p_portada,
        DateTime? p_fecha_publicacion,
        string p_categoria,
        string p_isbn,
        string p_slug,
        string p_thumbnail,
        out string p_resultado)
    {
        using (Entities dbContext = new Entities())
        {
            try
            {
                var libro = dbContext.LIBROes.FirstOrDefault(l => l.ID_LIBRO == p_id_libro);

                if (libro != null)
                {
                    libro.NOMBRE_LIBRO = p_nombre_libro;
                    libro.DESCRIPCION = p_descripcion;
                    libro.AUTOR = p_autor;
                    libro.EDITORIAL = p_editorial;
                    libro.PRECIO_UNITARIO = p_precio_unitario;
                    libro.CANTIDAD_DISPONIBLE = p_cantidad_disponible;
                    libro.PORTADA = p_portada;
                    libro.FECHA_PUBLICACION = p_fecha_publicacion;
                    libro.CATEGORIA = p_categoria;
                    libro.ISBN = p_isbn;
                    libro.SLUG = p_slug;
                    libro.THUMBNAIL = p_thumbnail;

                    dbContext.SaveChanges();

                    p_resultado = "Libro modificado correctamente";
                }
                else
                {
                    p_resultado = "No se encontró el libro con el ID especificado";
                }
            }
            catch (Exception ex)
            {
                p_resultado = $"Error al modificar el libro: {ex.Message}";
            }
        }
    }
    public static int GetAmountBooks()
    {
        try
        {
            using (Entities dbContext = new Entities())
            {
                var cantidadParam = new OracleParameter
                {
                    ParameterName = "cantidad",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.Int32
                };

                dbContext.Database.ExecuteSqlCommand("BEGIN MostrarCantidadLibros (:cantidad); END;", cantidadParam);

                var cantidadLibros = 0;

                if (cantidadParam.Value is OracleDecimal oracleDecimal)
                {
                    try
                    {
                        cantidadLibros = oracleDecimal.ToInt32();
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

                return cantidadLibros;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener la cantidad de libros: " + ex.Message);
            return 0;
        }
    }

    public static ObservableCollection<LIBRO> GetBooks()
    {
        try
        {
            using (var dbContext = new Entities())
            {
                List<LIBRO> libros = dbContext.LIBROes.ToList();
                return new ObservableCollection<LIBRO>(libros);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener lista de libros: {ex.Message}");
        }
    }
}