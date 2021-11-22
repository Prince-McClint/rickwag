using Microsoft.AspNetCore.Identity;

namespace WordJumble.Shared
{
    public class Player : IdentityUser
    {
        public List<Dictionary>? Dictionaries { get; set; }
    }
}
