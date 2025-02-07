using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Domain.Entities;
using WebScraper.Domain.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace WebScraper.Application.Services
{
    public class NewsScraperService
    {
        private readonly INewsRepository _newsRepository;
        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsScraperService> _logger;
        public NewsScraperService(INewsRepository newsRepository, ILogger<NewsScraperService> logger)
        {
            _newsRepository = newsRepository;
            _httpClient = new HttpClient();
            _logger = logger;   
        }

        public async Task ScrapeNewsAsync()
        {
            try
            {
                _logger.LogInformation("Starting to scrape news...");

                string url = "https://gamefa.com/";
                var response = await _httpClient.GetStringAsync(url);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("No response received from the website.");
                    return;
                }

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);


                var articles = htmlDoc.DocumentNode.SelectNodes("//div[@class='box ']");
                if (articles != null)
                {
                    foreach (var article in articles)
                    {
                        var titleNode = article.SelectSingleNode(".//h3[@class='mb-3 pl-2 pr-1 font-weight-bold']");
                        var descriptionNode = article.SelectSingleNode(".//div[@class='exerpt']//p");
                        var linkNode = article.SelectSingleNode(".//a[@class='d-block px-2']");

                        string title = titleNode?.InnerText.Trim() ?? "No title";
                        string description = descriptionNode?.InnerText.Trim() ?? "No Description";
                        string Url = linkNode?.GetAttributeValue("href", "") ?? "No Url";
                        if (!await _newsRepository.NewsExistsByUrlAsync(Url))
                        {
                            await _newsRepository.AddNewsAsync(new NewsArticle
                            {
                                Title = title,
                                Description = description,
                                Url = Url,
                                ScrapedAt = DateTime.UtcNow
                            });

                            _logger.LogInformation("Saved new article to the database.");
                        }
                        else
                        {
                            _logger.LogInformation("Article already exists in the database.");
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("No articles found on the page.");
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
