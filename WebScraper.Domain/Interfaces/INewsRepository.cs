using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Domain.Entities;

namespace WebScraper.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task AddNewsAsync(NewsArticle news);
        Task<bool> NewsExistsByWordAsync(string Word);
        Task<bool> NewsExistsByUrlAsync(string Url);
    }
}
