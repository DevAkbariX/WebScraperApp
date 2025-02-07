using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Domain.Entities;
using WebScraper.Domain.Interfaces;
using WebScraper.Infrastructure.Data;

namespace WebScraper.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;
        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddNewsAsync(NewsArticle news)
        {
            _context.NewsArticles.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NewsExistsByUrlAsync(string Url)
        {
            return await _context.NewsArticles.AnyAsync(n => n.Url == Url);
        }

        public async Task<bool> NewsExistsByWordAsync(string Word)
        {
            return await _context.NewsArticles.AnyAsync(n => n.Title.Contains(Word));
        }
    }
}
