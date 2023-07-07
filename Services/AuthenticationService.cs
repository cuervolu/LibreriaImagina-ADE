using dotenv.net;
using Newtonsoft.Json;
using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace SistemaLibreriaImagina.Services
{
    public class AuthenticationService
    {
        public static async Task<HttpResponseMessage> AuthenticateAsync(string username, string password)
        {
            DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: false));
            // Lógica para enviar las credenciales al servidor de autenticación y realizar la autenticación.
            // Devuelve la respuesta HTTP.
            var envVars = DotEnv.Read();


            // 1. Crear una solicitud HTTP al servidor de autenticación
            var request = new HttpRequestMessage(HttpMethod.Post, $"{envVars["API_URL"]}api/login/");

            // 2. Agregar los parámetros de inicio de sesión (username y password)
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };
            request.Content = new FormUrlEncodedContent(formData);

            // 3. Enviar la solicitud HTTP y recibir la respuesta
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);

            return response;
        }


        public static async Task<HttpResponseMessage> LogoutAsync(string token)
        {
            var envVars = DotEnv.Read();

            // Crear una solicitud HTTP para realizar el logout
            var request = new HttpRequestMessage(HttpMethod.Post, $"{envVars["API_URL"]}api/logout/");

            // Agregar el token como parámetro en la solicitud HTTP
            request.Headers.Add("Authorization", $"Token {token}");

            // Enviar la solicitud HTTP y recibir la respuesta
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);

            return response;
        }

        public async Task CreateUser(string rut, string nombre, string apellido, string username, string correo, string telefono, string tipoUsuario)
        {
            var envVars = DotEnv.Read();
            var httpClient = new HttpClient();

            try
            {
                // Verificar la existencia de usuario, correo y username en la base de datos
                bool isRutExists = await IsRutExists(rut);
                bool isUsernameExists = await IsUsernameExists(username);
                bool isCorreoExists = await IsCorreoExists(correo);

                if (isRutExists || isUsernameExists || isCorreoExists)
                {
                    // Al menos uno de los campos ya existe en la base de datos
                    throw new Exception("Uno o más campos ya existen en la base de datos");
                }

                string password = GeneratePassword(rut, nombre, apellido);

                var usuario = new
                {
                    rut,
                    nombre,
                    apellido,
                    username,
                    correo,
                    telefono,
                    tipo_usuario = tipoUsuario,
                    password
                };


                // Imprimir el objeto usuario en la consola
                Console.WriteLine(usuario);

                var jsonUsuario = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{envVars["API_URL"]}api/createUser/", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error {response.StatusCode} al crear el usuario: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el usuario: {ex.Message}");
                throw new Exception($"Error al crear el usuario: {ex.Message}");
            }
        }

        public string GeneratePassword(string rut, string nombre, string apellido)
        {
            string rutDigits = rut.Replace(".", "").Replace("-", ""); // Eliminar puntos y guión del RUT
            string rutPrefix = rutDigits.Substring(0, 3); // Obtener los primeros 3 dígitos del RUT
            string password = rutPrefix + nombre + "_" + apellido; // Construir la contraseña

            return password;
        }


        private async Task<bool> IsRutExists(string rut)
        {
            // Realizar la consulta en la base de datos para verificar la existencia del RUT
            using (var dbContext = new Entities())
            {
                return await dbContext.USUARIOs.AnyAsync(u => u.RUT == rut);
            }
        }

        private async Task<bool> IsUsernameExists(string username)
        {
            // Realizar la consulta en la base de datos para verificar la existencia del username
            using (var dbContext = new Entities())
            {
                return await dbContext.USUARIOs.AnyAsync(u => u.USERNAME == username);
            }
        }

        private async Task<bool> IsCorreoExists(string correo)
        {
            // Realizar la consulta en la base de datos para verificar la existencia del correo
            using (var dbContext = new Entities())
            {
                return await dbContext.USUARIOs.AnyAsync(u => u.CORREO == correo);
            }
        }




    }
}
