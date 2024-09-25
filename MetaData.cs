using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyDataManagement
{
    public sealed class MetaData {
        public string runName { get; set; } = "";
        public int index { get; set; } = 0;
        public string farmName { get; set; } = "";
        public string farmerName { get; set; } = "";
        public string petName { get; set; } = "";
        public string version { get; set; } = "";
        public string lastCategory { get; set; } = "";
        public string lastEvent { get; set; } = "";
        public string itemSprite { get; set; } = "";
        public string date { get; set; } = "";
        public string description { get; set; } = "";
        public string status { get; set; } = "";
    }
}
