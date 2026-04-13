using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class Player//設定玩家初始狀態，結構{}可被讀取更改，之後亦同
    {
        public string Name { get; set; } = "";
        public int HP { get; set; } = 100;
        public int MaxHP { get; set; } = 100;
        public int MP { get; set; } = 80;
        public int MaxMP { get; set; } = 80;
        public int Attack { get; set; } = 18;
        public int MagicAttack { get; set; } = 25;
        public int TempAttackBonus { get; set; } = 0;//暫時防禦用於之後當前回合debuff
        public int TempMagicAttackBonus { get; set; } = 0;
    }
}
