using FinningUrlShortner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinningUrlShortner.Data
{
    public interface IShortUrlRepository
    {
        //Validate URL
        Task<bool> UrlExists(string originalUrl);
        Task<ShortUrl> CreateUrl(string originalUrl);
        Task<ShortUrl> GetUrl(string originalUrl);
    }
}
