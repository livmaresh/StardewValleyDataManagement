using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyDataManagement
{
    public sealed class EventData
    {
        public EventData() {
            this.runName = "";
            this.id = new List<int>();
            this.runIndex = new List<int>();
            this.category = new List<string>();
            this.lastEvent = new List<string>();
            this.categorySprite = new List<string>();
            this.itemSprite = new List<string>();
            this.description = new List<string>();
            this.date = new List<string>();
        }

        public string runName { get; set; }
        public List<int> id { get; set; }
        public List<int> runIndex { get; set; }
        public List<string> category { get; set; }
        public List<string> lastEvent { get; set; }
        public List<string> categorySprite { get; set; }
        public List<string> itemSprite { get; set; }
        public List<string> description { get; set; }
        public List<string> date { get; set; }
    }
}
