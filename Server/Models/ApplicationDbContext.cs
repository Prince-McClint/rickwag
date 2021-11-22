
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WordJumble.Shared;

namespace WordJumble.Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<Player>
    {
        #region properties
        public DbSet<Word> Words { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
