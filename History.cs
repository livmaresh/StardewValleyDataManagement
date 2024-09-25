using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyDataManagement
{
    public sealed class History
    {
        public History() {
            this.categories = new List<string>();
            this.events = new List<string>();
        }

        public List<string> categories;
        public List<string> events;
    }
}
