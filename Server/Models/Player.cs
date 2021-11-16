using Microsoft.AspNetCore.Identity;

using WordJumble.Shared;

namespace WordJumble.Server.Models
{
    public class Player : IdentityUser
    {
        public List<Score>? Scores { get; set; }
        public List<Dictionary>? Dictionaries { get; set; }
    }
}
