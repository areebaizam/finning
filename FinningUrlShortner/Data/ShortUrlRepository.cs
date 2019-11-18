using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinningUrlShortner.Models;
using FinningUrlShortner.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinningUrlShortner.Data
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly DataContext _context;
        private IConfiguration _config { get; }
        private string Token { get; set; }
        public ShortUrlRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
       
        public async Task<ShortUrl> CreateUrl(string originalUrl)
        {
            //Generate TOKEN (Short URL)
            Token = Extensions.GenerateToken();
           
            //Create New URL
            var shortUrl = new ShortUrl()
            {
                OriginalURL = originalUrl,
                Token = Token,
                ShortURL = "finning.co/" + Token //Configure Base URL in App Settings or in Database
            };
            await _context.ShortUrls.AddAsync(shortUrl);
            await _context.SaveChangesAsync();
            return shortUrl;
        }

        public async Task<bool> UrlExists(string originalUrl)
        {
            if (await _context.ShortUrls.AnyAsync(x => x.OriginalURL == originalUrl))
                return true;
            return false;
        }


        public async Task<ShortUrl> GetUrl(string originalUrl)
        {
            var url = await _context.ShortUrls.FirstOrDefaultAsync(x => x.OriginalURL == originalUrl);
            return url;
        }
    }
}
