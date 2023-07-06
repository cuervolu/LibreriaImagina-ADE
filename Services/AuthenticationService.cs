using dotenv.net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<bool> createUser(string rut, string nombre, string apellido, string username, string correo, string telefono, string tipoUsuario)
        {
            var envVars = DotEnv.Read();
            var httpClient = new HttpClient();
            try
            {
                var usuario = new
                {
                    rut,
                    nombre,
                    apellido,
                    username,
                    correo,
                    telefono,
                    tipo_usuario = tipoUsuario
                };

                var jsonUsuario = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{envVars["API_URL"]}api/createUser/", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el usuario: {ex.Message}");
                return false;
            }
        }



    }
}
