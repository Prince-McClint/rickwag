using System.Net.Http.Json;
using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public class WordService : IWordService
    {
        #region fields
        private readonly HttpClient _httpClient;
        private int currentWordCount = 0;
        private const int MAXWORDLENGTH = 8;
        private const int MINWORDLENGTH = 3;
        #endregion

        #region properties
        public List<Word> CurrentWords { get; set; } = new List<Word>();
        public Dictionary CurrentDictionary { get; set; } = new Dictionary();
        #endregion

        #region methods
        public WordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public int GetCount() => CurrentWords.Count;

        public void ResetCurrentWordCount() => currentWordCount = 0;

        public Dictionary GetCurrentDictionary()
        {
            return CurrentDictionary;
        }

        public async Task SetCurrentDictionary(Dictionary dictionary, bool isApi, int level)
        {
            ResetCurrentWordCount();

            CurrentDictionary = dictionary;

            if (!isApi)
                await SetCurrentWords(CurrentDictionary.DictionaryID, level);
            else
            {
                CurrentWords = FilterWordsBySize(dictionary.Words);
            }
        }

        private async Task SetCurrentWords(int currentDicID, int level)
        {
            CurrentWords = FilterWordsBySize(FilterWordsByLevel(await GetWordsFromDictionary(currentDicID), level));
        }

        private List<Word> FilterWordsBySize(List<Word> words)
        {
            List<Word> newWords = new List<Word>();

            foreach (Word word in words)
            {
                var wordContent = word.WordContent;

                if (wordContent.Length > MAXWORDLENGTH || wordContent.Length < MINWORDLENGTH)
                    continue;

                newWords.Add(word);
            }

            return newWords;
        }

        private List<Word> FilterWordsByLevel(List<Word> words, int level)
        {
            List<Word> newWords = new List<Word>();

            foreach (Word word in words)
            {
                var wordLevel = word.Level;

                if (wordLevel != level)
                    continue;

                newWords.Add(word);
            }

            return newWords;
        }

        public Word GetNextWord()
        {
            var word = new Word();

            foreach (var wd in CurrentWords)
            {
                Console.WriteLine(wd.WordContent);
            }

            if (currentWordCount < CurrentWords?.Count)
                word = CurrentWords[currentWordCount++];

            return word;
        }

        public async Task<Dictionary> GetDictionary(int DictionaryID)
        {
            var requestUri = $"api/words/GetDictionary/{DictionaryID}";

            return await _httpClient.GetFromJsonAsync<Dictionary>(requestUri);
        }

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

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        return body;
                    }
                    catch (HttpRequestException e)
                    {

                        return "failed";
                    }
                }
            }
            catch (System.Exception)
            {

                return "failed";
            }
        }

        public async Task<string> GetWords(int count, bool canSwear)
        {
            var swear = canSwear ? 1 : 0;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://random-word-api.herokuapp.com/word?number={count}&swear={swear}")
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                return body;
            }
        }

        public async Task<List<Dictionary>> GetDictionaries()
        {
            var requestUri = "api/words/GetDictionaries";

            return await _httpClient.GetFromJsonAsync<List<Dictionary>>(requestUri);
        }

        public async Task<List<Dictionary>> GetPlayerDictionaries()
        {
            var requestUri = "api/words/GetPlayerDictionaries";

            return await _httpClient.GetFromJsonAsync<List<Dictionary>>(requestUri);
        }

        public async Task AddDictionaryToCurrentPlayer(Dictionary dictionary)
        {
            var requestUri = "api/words/AddDictionaryToCurrentPlayer";

            var result = await _httpClient.PostAsJsonAsync(requestUri, dictionary);

            result.EnsureSuccessStatusCode();
        }

        public async Task<Player> GetCurrentPlayer()
        {
            var requestUri = "api/words/GetCurrentPlayer";

            var player = await _httpClient.GetFromJsonAsync<Player>(requestUri);

            return player;
        }

        public async Task<List<Word>> GetWordsFromDictionary(int id)
        {
            var requestUri = $"api/words/GetWordsFromDictionary/{id}";

            return await _httpClient.GetFromJsonAsync<List<Word>>(requestUri);
        }

        public async Task UpdateDictionary(Dictionary dictionary)
        {
            var requestUri = "api/words/UpdateDictionary";

            await _httpClient.PutAsJsonAsync<Dictionary>(requestUri, dictionary);
        }

        public async Task DeleteDictionary(int id)
        {
            var requestUri = $"api/words/DeleteDictionary/{id}";

            await _httpClient.DeleteAsync(requestUri);
        }

        public async Task SavePlayerScore(int score)
        {
            var newScore = new Score { Value = score };

            var requestUri = "api/words/SavePlayerScore";

            var result = await _httpClient.PostAsJsonAsync<Score>(requestUri, newScore);
            result.EnsureSuccessStatusCode();
        }

        public async Task<List<PlayerScore>> GetPlayersScores()
        {
            var requestUri = "api/words/GetPlayersScores";

            return await _httpClient.GetFromJsonAsync<List<PlayerScore>>(requestUri);
        }

        public async Task<string> GetDictionaryCreator(int dictionaryID)
        {
            var requestUri = $"api/words/GetDictionaryCreator/{dictionaryID}";

            return await _httpClient.GetStringAsync(requestUri);
        }
        #endregion
    }
}
