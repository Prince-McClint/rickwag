using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public interface IWordService
    {
        public Task SetCurrentDictionary(Dictionary dictionary);
        public Word GetNextWord();
        public Task<Dictionary> GetDictionary(int ID);
        public Task<string> GetWordMeaning(string word);
        public Task<List<Dictionary>> GetDictionaries();
        public Task<List<Dictionary>> GetPlayerDictionaries();
        public Task AddDictionaryToCurrentPlayer(Dictionary dictionary);
        public Task<List<Word>> GetWordsFromDictionary(int ID);
        public Task<Player> GetCurrentPlayer();
        public Task UpdateDictionary(Dictionary dictionary);
        public Task DeleteDictionary(int dictionaryID);
    }
}
