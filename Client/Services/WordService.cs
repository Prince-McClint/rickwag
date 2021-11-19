using System.Net.Http.Json;

using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public class WordService : IWordService
    {
        #region fields
        private readonly HttpClient _httpClient;
        private int currentWordCount = 0;
        #endregion

        #region properties
        public List<Word> CurrentWords { get; set; }
        #endregion

        #region methods
        public WordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Word>> GetWords()
        {
            var requestUri = "api/Words/GetWords";

            return await _httpClient.GetFromJsonAsync<List<Word>>(requestUri);
        }

        public async Task SetCurrentDictionary()
        {
            CurrentWords = await GetWords();

            Console.WriteLine($"count => {CurrentWords.Count}");
        }

        public Word GetNextWord()
        {
            return CurrentWords[currentWordCount++];
        }

        public async Task<Dictionary> GetDictionary(int DictionaryID)
        {
            var requestUri = $"api/words/getdictionary/{DictionaryID}";

            Console.WriteLine("before json");

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
