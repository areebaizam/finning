using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinningUrlShortner.Data;
using FinningUrlShortner.DTOs;
using FinningUrlShortner.Models;
using FinningUrlShortner.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace FinningUrlShortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlsController : ControllerBase
    {
        private readonly IShortUrlRepository _repo;
        private readonly ILogger _logger;

        public ShortUrlsController(IShortUrlRepository repo, ILogger<ShortUrlsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [Authorize(Roles = "admin")]//Role was added in Claims
        [HttpPost]
        public async Task<IActionResult> ShortenUrlCreate(ShortUrlInDTO originalUrl)///Query params can be null so add hint
        {
            //Check URL in Request;
            if (!ModelState.IsValid)
                return BadRequest("URL not present in the request");

            //Check URL already Exists
            if (await _repo.UrlExists(originalUrl.OriginalURL))
                return BadRequest("Shortened URL already exists for "+ originalUrl.OriginalURL);

            //Generate and Save short URL
            var shortUrl = await _repo.CreateUrl(originalUrl.OriginalURL);
            return Ok(shortUrl.ShortURL);
        }

        [HttpGet]
        public async Task<IActionResult> ShortenUrlGet(ShortUrlInDTO originalUrl)///Query params can be null so add hint
        {
            //Check URL in Request;
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid request. Url sent was: {originalUrl}", originalUrl);
                return BadRequest("URL not present in the request");
            }

            //Check URL already Exists
            if (!await _repo.UrlExists(originalUrl.OriginalURL))
                return BadRequest("Shortened URL does not exists for " + originalUrl.OriginalURL);

            //Generate and Save short URL
            var shortUrl = await _repo.GetUrl(originalUrl.OriginalURL);
            return Ok(shortUrl.ShortURL);
        }
    }
}