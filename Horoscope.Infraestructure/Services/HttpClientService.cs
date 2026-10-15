using Horoscope.Application.Dtos.NewAstro;
using System.Text.Json;
using System.Text;
using Horoscope.Application.Dtos;

namespace Horoscope.Infraestructure.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _client;

        public HttpClientService(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Estructura y setea los datos necesarios para realizar la peticion a API 'newastro'
        /// </summary>
        /// <param name="request"></param>
        /// <returns>El objeto listo para request a API</returns>
        public NewAstroRequestDto CreateBodyToAPI(HoroscopeRequestDto request)
        {
            // body enviado a la API de newastro
            return new NewAstroRequestDto
            {
                date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                lang = request.Lang?.Trim().ToLowerInvariant(),
                //sign = _horoscopeService.GetZodiacSign(request.BirthDate)
            };
        }

        /// <summary>
        /// Realiza la peticion a newastro API
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>La respuesta serealizada</returns>
        public async Task<string> GetResponseFromNewAstroAPI(NewAstroRequestDto payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
