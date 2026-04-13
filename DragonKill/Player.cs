using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class Player
    {
        public string Name { get; set; } = "";
        public int HP { get; set; } = 100;
        public int MaxHP { get; set; } = 100;
        public int MP { get; set; } = 80;
        public int MaxMP { get; set; } = 80;
        public int Attack { get; set; } = 18;
        public int MagicAttack { get; set; } = 25;
        public int TempAttackBonus { get; set; } = 0;
        public int TempMagicAttackBonus { get; set; } = 0;
    }
}
