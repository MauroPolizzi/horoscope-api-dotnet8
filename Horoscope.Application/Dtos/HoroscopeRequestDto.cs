namespace Horoscope.Application.Dtos
{
    public class HoroscopeRequestDto
    {
        public string Name { get; set; } = null!;
        [EmailField]
        public string Email { get; set; } = string.Empty;
        public int Gender { get; set; } = 0;
        //public string Sign { get; set; } = null!;
        public string? Lang { get; set; } = "es";
        public DateTime BirthDate { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class EmailFieldAttribute: Attribute
    { }
}
