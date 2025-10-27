using Horoscope.Domain.Utils;

namespace Horoscope.Domain.Entities
{
    public class HoroscopeQuery
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Gender Gender { get; set; } = Gender.None;
        public string Email { get; set; } = null!;
        public string Sign { get; set; } = null!;
        public string Lang { get; set; } = "es";
        public DateTime BirthDate { get; set; }
        public DateTime DateQueried { get; set; } = DateTime.Now;
        public string ResponseJson { get; set; } = null!;
    }
}
