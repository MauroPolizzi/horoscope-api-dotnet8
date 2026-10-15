namespace Horoscope.Application.Dtos.NewAstro
{
    public class NewAstroRequestDto
    {
        public string date { get; set; } = string.Empty;
        public string? lang { get; set; } = string.Empty;
        public string sign { get; set; } = string.Empty;
    }
}
