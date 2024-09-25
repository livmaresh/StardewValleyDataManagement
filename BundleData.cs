using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyDataManagement
{
    public sealed class BundleData
    {
        public BundleData() {
            this.name = new List<string>();
            this.location = new List<string>();
            this.bundleString = new List<string>();
            this.items = new List<List<string>> { };
            this.flags = new List<List<bool>> { };
        }

        public List<string> name { get; set; }
        public List<string> location { get; set; }
        public List<string> bundleString { get; set; }
        public List<List<string>> items { get; set; }
        public List<List<bool>> flags { get; set; }
    }
}
