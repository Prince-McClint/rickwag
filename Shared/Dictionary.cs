using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WordJumble.Shared
{
    public class Dictionary
    {
        public int DictionaryID { get; set; }

        [Required]
        public string DictionaryName { get; set; }

        public int LevelsCount { get; set; } = 3;

        //relationship properties
        public List<Word>? Words { get; set; }

        public string? PlayerID { get; set; }
        public Player? Player { get; set; }
    }
}
