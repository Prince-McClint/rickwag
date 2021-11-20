using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public interface IWordService
    {
        public Task<List<Word>> GetWords();
        public Task SetCurrentDictionary(Dictionary dictionary);
        public Word GetNextWord();
        public Task<Dictionary> GetDictionary(int ID);
        public Task<string> GetWordMeaning(string word);
        public Task<List<Dictionary>> GetDictionaries();
    }
}
