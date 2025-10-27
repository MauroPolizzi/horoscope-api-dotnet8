using Horoscope.Application.Dtos;
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
        private readonly HttpClient _client;
        private readonly HoroscopeRepository _repository;
        private readonly ILogger<HoroscopeService> _logger;

        public HoroscopeService(HttpClient client, HoroscopeRepository repository, ILogger<HoroscopeService> logger)
        {
            _client = client;
            _repository = repository;
            _logger = logger;
        }

        public async Task<HoroscopeResponseDto> GetDailyHoroscopeAsync(HoroscopeRequestDto request)
        {
            var validate = new ValidatorService();
            List<string> errors = validate.Validate(request);

            if (errors.Any()) return new HoroscopeResponseDto
            {
                Errors = errors
            };

            // Obtenemos el signo
            string sign = GetZodiacSign(request.BirthDate);

            // body enviado a la API de newastro
            var payload = new
            { 
                date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                lang = request.Lang?.Trim().ToLowerInvariant(),
                sign = sign.ToLower()
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(responseBody);

            var query = new HoroscopeQuery
            {
                Name = request.Name,
                Sign = sign,
                Lang = request.Lang,
                BirthDate = request.BirthDate,
                ResponseJson = responseBody,
                Email = request.Email,
                Gender = (Gender)request.Gender,
            };
            
            // Insertamos en DB
            await _repository.AddAsync(query);

            // Obtenemos la informacion del horoscopo de su porpiedad horoscope
            var horoscopeText = doc.RootElement.GetProperty("horoscope").GetString() ?? string.Empty;

            int daysToBirthday = GetDaysToBirthday(request.BirthDate);

            return new HoroscopeResponseDto
            {
                Greeting = $"Hola {request.Name}, tu horóscopo de hoy es:",
                Sing = sign,
                DaysToBirthday = daysToBirthday,
                HoroscopeText = horoscopeText,
                Errors = [],
            };
        }

        public async Task<List<StatisticsSignResponseDto>> GetStatisticsSignAsync()
        {
            return await _repository.GetStatisticsSignAsync();
        }

        public async Task<List<HoroscopeQueryDto>> GetAllHistoryAsync()
        {
            return await _repository.GetAllAsync();
        }

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

    }
}
