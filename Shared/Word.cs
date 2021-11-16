using System.ComponentModel.DataAnnotations;

namespace WordJumble.Shared
{
    public class Word
    {
        public int WordID { get; set; }

        [Required]
        public string? WordContent { get; set; }
        public string? Meaning { get; set; }

        public List<Score>? Scores { get; set; }
    }
}
