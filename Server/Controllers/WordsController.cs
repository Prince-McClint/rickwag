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

        [HttpGet("{id:int}")]
        public Dictionary GetDictionary(int id)
        {
            var dictionary = _context.Dictionaries.FirstOrDefault(dic => dic.DictionaryID == id);

            return dictionary;
        }

        [HttpGet]
        public IList<Dictionary> GetDictionaries()
        {
            return _context.Dictionaries.ToArray();
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
            var player = await _userManager.FindByNameAsync(User.Identity?.Name);

            if (player != null)
            {
                if (player.Dictionaries == null)
                    player.Dictionaries = new List<Dictionary>();

                player.Dictionaries.Add(dictionary);

                var result = await _userManager.UpdateAsync(player);

                if (result.Succeeded)
                {
                    var playerAfter = await _userManager.FindByNameAsync(User.Identity?.Name);

                    if (player != null)
                    {
                        System.Console.WriteLine($"saved {player} dictionary count => {player.Dictionaries?.Count}");
                    }
                }
            }
        }

        [HttpGet("{id:int}")]
        public IList<Word> GetWordsFromDictionary(int id)
        {
            List<Word> words = _context.Words.ToList();
            List<Word> dictionaryWords = new List<Word>();

            foreach (var word in words)
                if (word.DictionaryID == id) dictionaryWords.Add(word);


            return dictionaryWords;
        }

        [HttpGet]
        public async Task<Player> GetCurrentPlayer()
        {
            var playerID = _userManager.GetUserId(User);

            var player = await _userManager.FindByIdAsync(playerID);

            return player;
        }


        [HttpPut]
        public void UpdateDictionary(Dictionary dictionary)
        {
            var existingDictionary = _context.Dictionaries.First(dic => dic.DictionaryID == dictionary.DictionaryID);
            var existingWords = GetWordsFromDictionary(existingDictionary.DictionaryID);
            var wordsToAdd = dictionary.Words;

            //to handle deleting
            foreach (var word in existingWords)
            {
                if (!(dictionary.Words.Contains(word))) _context.Words.Remove(word);
            }

            existingDictionary.DictionaryName = dictionary.DictionaryName;
            existingDictionary.Words = wordsToAdd;

            _context.SaveChanges();
        }

        [HttpDelete("{id:int}")]
        public void DeleteDictionary(int id)
        {
            var dictionary = _context.Dictionaries.First(dic => dic.DictionaryID == id);
            var dicName = dictionary.DictionaryName;

            _context.Dictionaries.Remove(dictionary);

            _context.SaveChanges();
        }

        [HttpGet]
        public IList<Dictionary> GetPlayerDictionaries()
        {
            var playerDictionaries = new List<Dictionary>();
            var dictionaries = GetDictionaries();

            foreach (var dictionary in dictionaries)
            {
                if (dictionary.PlayerID == _userManager.GetUserId(User)) playerDictionaries.Add(dictionary);
            }

            return playerDictionaries;
        }

        [HttpPost]
        public async Task SavePlayerScore(Score score)
        {
            if (User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var player = await GetCurrentPlayer();

                    player.Score += score.Value;

                    await _userManager.UpdateAsync(player);
                }
            }
        }

        [HttpGet]
        public List<PlayerScore> GetPlayersScores()
        {
            IList<Player> players = _userManager.Users.ToArray();
            List<PlayerScore> PlayersScores = new List<PlayerScore>();

            foreach (var player in players)
            {
                var playerScore = new PlayerScore
                {
                    Username = player.UserName,
                    Score = player.Score
                };

                PlayersScores.Add(playerScore);
            }


            return PlayersScores;
        }

        [HttpGet("{id:int}")]
        public async Task<string> GetDictionaryCreator(int id)
        {
            var dictionary = _context.Dictionaries.FirstOrDefault(dic => dic.DictionaryID == id);

            if (dictionary == null)
                return "no creator";

            var playerID = dictionary.PlayerID;

            var player = await _userManager.FindByIdAsync(playerID);

            return player != null ? player.UserName : "no creator";
        }
        #endregion
    }
}
