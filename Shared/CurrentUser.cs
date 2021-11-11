using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordJumble.Shared
{
    public class CurrentUser
    {
        public string? Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public Dictionary<string, string>? Claims { get; set; }
    }
}
