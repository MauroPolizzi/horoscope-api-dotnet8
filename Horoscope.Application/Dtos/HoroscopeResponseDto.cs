namespace Horoscope.Application.Dtos
{
    public class HoroscopeResponseDto
    {
        public string Greeting { get; set; } = string.Empty;
        public string Sing { get; set; } = string.Empty;
        public int DaysToBirthday { get; set; }
        public string HoroscopeText { get; set; } = string.Empty;
        public List<string> Errors { get; set; }
    }
}
