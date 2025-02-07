using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebScraper.Application.Services;

namespace WebScraper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperTestController : ControllerBase
    {
        private readonly NewsScraperService _newsScraperService;

        public ScraperTestController(NewsScraperService newsScraperService)
        {
            _newsScraperService = newsScraperService;
        }

        [HttpGet("test-scrape")]
        public async Task<IActionResult> TestScrape()
        {
            await _newsScraperService.ScrapeNewsAsync();
            return Ok("Scraping completed");
        }
    }
}
