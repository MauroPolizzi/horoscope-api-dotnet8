using Azure.Core;
using Horoscope.Application.Dtos;
using Horoscope.Application.Dtos.NewAstro;
using Horoscope.Application.Interfaces;
using Horoscope.Domain.Entities;
using Horoscope.Domain.Utils;
using Horoscope.Infraestructure.Repositories;
using Horoscope.Infraestructure.Validators;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Horoscope.Infraestructure.Services
{
    public class HoroscopeService : IHoroscopeService
    {
        private readonly HttpClientService _clientService;
        private readonly HoroscopeRepository _repository;
        private readonly ILogger<HoroscopeService> _logger;
        private List<string> _requestErrors = new List<string>();

        public HoroscopeService(HttpClientService clientService, HoroscopeRepository repository, ILogger<HoroscopeService> logger)
        {
            _clientService = clientService;
            _repository = repository;
            _logger = logger;
        }

        public async Task<HoroscopeResponseDto> GetDailyHoroscopeAsync(HoroscopeRequestDto request)
        {
            try
            {
                // validamos request
                if (!IsValid(request)) return new HoroscopeResponseDto() { Errors = _requestErrors };

                // Obtenemos el body para la request a newastro API
                NewAstroRequestDto payload = CreateBodyToAPI(request);

                // Llamada a newastro API
                var responseBody = GetResponseAPI(payload);
                var doc = JsonDocument.Parse(responseBody.Result);

                // Insertamos el registro en DB
                AddRegisterRequestQuery(request, responseBody.Result);

                // Obtenemos la informacion del horoscopo de su porpiedad horoscope
                var horoscopeText = doc.RootElement.GetProperty("horoscope").GetString() ?? string.Empty;

                // Devolvemos la respuesta
                return new HoroscopeResponseDto
                {
                    Greeting = $"Hola {request.Name}, tu horóscopo de hoy es:",
                    Sing = GetZodiacSign(request.BirthDate),
                    DaysToBirthday = GetDaysToBirthday(request.BirthDate),
                    HoroscopeText = horoscopeText,
                    Errors = [],
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StatisticsSignResponseDto>> GetStatisticsSignAsync()
        {
            return await _repository.GetStatisticsSignAsync();
        }

        public async Task<List<HoroscopeQueryDto>> GetAllHistoryAsync()
        {
            return await _repository.GetAllAsync();
        }

        #region Metodos privados

        /// <summary>
        /// Valida que los campos obligatorios contengan valor y sean del tipo correspondiente
        /// </summary>
        /// <param name="request"></param>
        /// <returns>True en caso que sea valida</returns>
        private bool IsValid(HoroscopeRequestDto request)
        {
            ValidatorService validate = new ValidatorService();
            _requestErrors = validate.Validate(request);

            if (_requestErrors.Any()) return false;
            return true;
        }

        /// <summary>
        /// Estructura y setea los datos necesarios para realizar la peticion a API 'newastro'
        /// </summary>
        /// <param name="request"></param>
        /// <returns>El objeto listo para request a API</returns>
        private NewAstroRequestDto CreateBodyToAPI(HoroscopeRequestDto request) 
        {
            // body enviado a la API de newastro
            return new NewAstroRequestDto
            {
                date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                lang = request.Lang?.Trim().ToLowerInvariant(),
                sign = GetZodiacSign(request.BirthDate)
            };
        }

        private async Task<string> GetResponseAPI(NewAstroRequestDto payload)
        {
            return await _clientService.GetResponseFromNewAstroAPI(payload);
        }

        /// <summary>
        /// Inserta el registro de HoroscopeQuery en base de datos
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseBodyResult"></param>
        private async void AddRegisterRequestQuery(HoroscopeRequestDto request, string responseBodyResult)
        {
            var query = new HoroscopeQuery
            {
                Name = request.Name,
                Sign = GetZodiacSign(request.BirthDate),
                Lang = request.Lang,
                BirthDate = request.BirthDate,
                ResponseJson = responseBodyResult,
                Email = request.Email,
                Gender = (Gender)request.Gender,
            };

            // Insertamos en DB
            await _repository.AddAsync(query);
        }

        /// <summary>
        /// Obtiene el signo zoodiacal, segun la fecha proporcionada
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>El nombre del signo</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private string GetZodiacSign(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            return month switch
            {
                1 => (day <= 19) ? "Capricorn" : "Aquarius",
                2 => (day <= 18) ? "Aquarius" : "Pisces",
                3 => (day <= 20) ? "Pisces" : "Aries",
                4 => (day <= 19) ? "Aries" : "Taurus",
                5 => (day <= 20) ? "Taurus" : "Gemini",
                6 => (day <= 20) ? "Gemini" : "Cancer",
                7 => (day <= 22) ? "Cancer" : "Leo",
                8 => (day <= 22) ? "Leo" : "Virgo",
                9 => (day <= 22) ? "Virgo" : "Libra",
                10 => (day <= 22) ? "Libra" : "Scorpio",
                11 => (day <= 21) ? "Scorpio" : "Sagittarius",
                12 => (day <= 21) ? "Sagittarius" : "Capricorn",
                _ => throw new ArgumentOutOfRangeException(nameof(birthDate), "Fecha inválida")
            };
        }

        private int GetDaysToBirthday(DateTime birthDate)
        {
            var today = DateTime.Today;
            var next = new DateTime(today.Year, birthDate.Month, birthDate.Day);
            if (next < today) next = next.AddYears(1);
            return (next - today).Days;
        }

        #endregion

    }
}
