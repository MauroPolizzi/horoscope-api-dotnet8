using Horoscope.Application.Dtos;
using Horoscope.Domain.Entities;
using Horoscope.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Horoscope.Infraestructure.Repositories
{
    public class HoroscopeRepository
    {
        private readonly HoroscopeDbContext _context;
        public HoroscopeRepository(HoroscopeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(HoroscopeQuery entity)
        {
            await _context.HoroscopeQueries.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HoroscopeQueryDto>> GetAllAsync()
        {
            return await _context.HoroscopeQueries
                .Select(q => new HoroscopeQueryDto
                {
                    Name = q.Name,
                    Email = q.Email,
                    Sign = q.Sign,
                    BirthDate = q.BirthDate.ToShortDateString(),
                    DateQueried = q.DateQueried.ToShortDateString(),
                    ResponseJson = q.ResponseJson,
                })
                //.OrderByDescending(q => q.DateQueried)
                .ToListAsync();
        }

        public async Task<List<StatisticsSignResponseDto>> GetStatisticsSignAsync()
        {
            return await _context.HoroscopeQueries
                .GroupBy(q => q.Sign)
                .Select(g => new StatisticsSignResponseDto
                {
                    Sing = g.Key,
                    SearchCounter = g.Count()
                })
                .OrderByDescending(x => x.SearchCounter)
                .ToListAsync();
        }
    }
}
