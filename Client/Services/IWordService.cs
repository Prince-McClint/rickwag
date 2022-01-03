using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public interface IWordService
    {
        public Task SetCurrentDictionary(Dictionary dictionary, bool isApi = false, int level = 1);
        public Dictionary GetCurrentDictionary();
        public int GetCount();
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
        public Task SavePlayerScore(int score);
        public Task<List<PlayerScore>> GetPlayersScores();
        public Task<string> GetDictionaryCreator(int dictionaryID);
        public Task<string> GetWords(int count = 10, bool canSwear = false);
        public void ResetCurrentWordCount();
    }
}
