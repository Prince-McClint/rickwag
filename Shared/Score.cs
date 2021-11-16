using System.ComponentModel.DataAnnotations;

namespace WordJumble.Shared
{
    public class Score
    {
        public int ScoreID { get; set; }

        [Required]
        public int ScoreValue { get; set; }
    }
}
