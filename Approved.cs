using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyDataManagement
{
    public sealed class Approved
    {
        public Approved() { 
            this.farmerNames = new List<string>();
            this.farmNames = new List<string>();
        }

        public List<String> farmerNames { get; set; }
        public List<String> farmNames { get; set; }
    }
}
