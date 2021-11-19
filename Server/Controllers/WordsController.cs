using Microsoft.AspNetCore.Mvc;

using WordJumble.Server.Models;
using WordJumble.Shared;

namespace WordJumble.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        #region fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region methods
        public WordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IList<Word> GetWords()
        {
            return _context.Words.ToArray();
        }

        [HttpGet]
        public Dictionary GetDictionary(int id)
        {
            Console.WriteLine("in controller getting dictionary");
            return _context.Dictionaries.First();
        }

        [HttpGet]
        public async Task<string> GetWordMeaning(string word)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://mashape-community-urban-dictionary.p.rapidapi.com/define?term={word}"),
                Headers =
                {
                    { "x-rapidapi-host", "mashape-community-urban-dictionary.p.rapidapi.com" },
                    { "x-rapidapi-key", "826ac1aa91mshbc9f7d3a8ee6f76p1d79b1jsn8c91375bca71" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);

                return body;
            }
        }
        #endregion
    }
}
