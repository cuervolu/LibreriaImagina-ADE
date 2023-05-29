using dotenv.net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace SistemaLibreriaImagina.Services
{
    public static class AuthenticationService
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


    }
}
