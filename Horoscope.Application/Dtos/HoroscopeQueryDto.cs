using Horoscope.Domain.Utils;

namespace Horoscope.Application.Dtos
{
    public class HoroscopeQueryDto
    {
        public string Name { get; set; } = null!;
        public Gender Gender { get; set; }
        public string Email { get; set; } = null!;
        public string Sign { get; set; } = null!;
        public string BirthDate { get; set; }
        public string DateQueried { get; set; }
        public string ResponseJson { get; set; } = null!;
    }
}
