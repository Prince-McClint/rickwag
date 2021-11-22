using System.ComponentModel.DataAnnotations;

namespace WordJumble.Shared
{
    public class Word
    {
        public int WordID { get; set; }

        [Required]
        public string WordContent { get; set; }
        public string? Meaning { get; set; }

        //relationship properties
        public int DictionaryID { get; set; }
        public Dictionary? Dictionary { get; set; }
    }
}
