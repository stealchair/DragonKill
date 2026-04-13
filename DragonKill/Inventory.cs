using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class Inventory
    {
        public bool HasSword { get; set; } = false;
        public bool HasStaff { get; set; } = false;
        public bool HasDragonHeart { get; set; } = false;
        public bool HasKey { get; set; } = false;

        public int PotionCount { get; set; } = 0;
    }
}
