using Microsoft.AspNetCore.Identity;

namespace WordJumble.Shared
{
    public class Player : IdentityUser
    {
        //relationship properties
        public int Score { get; set; } = 0;

        public List<Dictionary>? Dictionaries { get; set; }
    }
}
