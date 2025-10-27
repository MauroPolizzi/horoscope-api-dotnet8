using Horoscope.Application.Dtos;
using Horoscope.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Horoscope.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoroscopeController : ControllerBase
    {
        private readonly IHoroscopeService _service;

        public HoroscopeController(IHoroscopeService service)
        {
            _service = service;
        }

        // POST: api/horoscope/gethoroscope
        [HttpPost("gethoroscope")]
        public async Task<IActionResult> GetHoroscope([FromBody] HoroscopeRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.GetDailyHoroscopeAsync(request);
            return Ok(result);
        }

        [HttpGet("getstatistics")]
        public async Task<IActionResult> GetStatisticsSign()
        {
            var result = await _service.GetStatisticsSignAsync();
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllHistory()
        {
            var result = await _service.GetAllHistoryAsync();
            return Ok(result);
        }
    }
}
