using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WordJumble.Shared
{
    public class Dictionary
    {
        public int DictionaryID { get; set; }

        [Required]
        public string? DictionaryName { get; set; }

        public List<Word>? Words { get; set; }
    }
}
