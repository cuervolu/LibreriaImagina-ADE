using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SistemaLibreriaImagina.Services
{
    public static class AuthenticationService
    {
        public static async Task<HttpResponseMessage> AuthenticateAsync(string username, string password)
        {
            // Lógica para enviar las credenciales al servidor de autenticación y realizar la autenticación.
            // Devuelve la respuesta HTTP.

            // 1. Crear una solicitud HTTP al servidor de autenticación
            var request = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:8000/api/login/");

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
    }
}
