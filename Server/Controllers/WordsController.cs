using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Player> _userManager;
        #endregion

        #region methods
        public WordsController(ApplicationDbContext context, UserManager<Player> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IList<Word> GetWords()
        {
            return _context.Words.ToArray();
        }

        [HttpGet]
        public Dictionary GetDictionary(int id)
        {
            var dictionary = _context.Dictionaries.First();


            return dictionary;
        }

        [HttpGet]
        public IList<Dictionary> GetDictionaries()
        {
            return _context.Dictionaries.ToArray();
        }

        [HttpGet]
        public Dictionary<string, List<Dictionary>> GetUserDictionaries()
        {
            var userDictionary = new Dictionary<string, List<Dictionary>>();

            foreach (var user in _userManager.Users)
            {
                userDictionary.Add(user.UserName, user.Dictionaries);
            }

            return userDictionary;
        }

        [HttpGet]
        public async Task<IList<Dictionary>> GetCurrentPlayerDictionaries()
        {
            IList<Dictionary> dictionaries = new List<Dictionary>();

            if (User.Identity.IsAuthenticated)
            {
                var player = await _userManager.FindByNameAsync(User.Identity.Name);

                if (player.Dictionaries != null)
                    dictionaries = player.Dictionaries.ToArray();
            }

            return dictionaries;
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

        [HttpPost]
        public async Task AddDictionaryToCurrentPlayer(Dictionary dictionary)
        {
            Console.WriteLine($"taking in {dictionary}");

            if (User.Identity.IsAuthenticated)
            {
                var player = await _userManager.FindByNameAsync(User.Identity.Name);

                if (player.Dictionaries == null)
                    player.Dictionaries = new List<Dictionary>();

                player.Dictionaries.Add(dictionary);

                await _userManager.UpdateAsync(player);

                Console.WriteLine($"{dictionary.DictionaryName} dictionary counts => {player.Dictionaries.Count}");

                Console.WriteLine("random stuff");
            }
        }
        #endregion
    }
}
