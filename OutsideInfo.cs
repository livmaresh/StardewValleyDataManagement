﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyDataManagement
{
    public sealed class OutsideInfo
    {
        public OutsideInfo() {
            this.ids = new List<int>();
            this.indexes = new List<int>();
            this.ids.Add(0);
            this.indexes.Add(0);
            this.indexes.Add(0);
        }

        public List<int> ids;
        public List<int> indexes;
    }
}
