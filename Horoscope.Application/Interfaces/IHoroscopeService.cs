using Horoscope.Application.Dtos;

namespace Horoscope.Application.Interfaces
{
    public interface IHoroscopeService
    {
        Task<HoroscopeResponseDto> GetDailyHoroscopeAsync(HoroscopeRequestDto request);
        Task<List<StatisticsSignResponseDto>> GetStatisticsSignAsync();
        Task<List<HoroscopeQueryDto>> GetAllHistoryAsync();
    }
}
