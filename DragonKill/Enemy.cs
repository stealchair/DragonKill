using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    // 行動種類
    public enum EnemyActionType
    {
        Attack,
        PhysicalDefense,
        MagicDefense,
        Special
    }

    // 單一怪物行動狀態
    public class EnemyAction
    {
        public string Name { get; set; } = "";
        public EnemyActionType Type { get; set; }
        public int Chance { get; set; }   // 機率加起來要 100
        public int Power { get; set; }    // 傷害、防禦、特殊效果倍率
    }

    // 怪物本體
    public class Enemy
    {
        public string Name { get; set; } = "";
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int CritChance { get; set; }   // 20 = 20% 
        public int PhysicalDefense { get; set; }
        public int MagicDefense { get; set; }
        public List<EnemyAction> Actions { get; set; } = new List<EnemyAction>();
        public int TempPhysicalDefense { get; set; } = 0;
        public int TempMagicDefense { get; set; } = 0;

        // 戰鬥中狀態
        public bool IsPhysicalDefending { get; set; } = false;
        public bool IsMagicDefending { get; set; } = false;
    }

    // 五隻怪物資料
    public static class EnemyFactory
    {
        public static Enemy CreateRootWolf()
        {
            return new Enemy
            {
                Name = "樹根狼",
                HP = 30,
                MaxHP = 30,
                PhysicalDefense = 5,
                MagicDefense = 5,
                CritChance = 20,

                Actions = new List<EnemyAction>
                {
                    new EnemyAction
                    {
                        Name = "撕咬",
                        Type = EnemyActionType.Attack,
                        Chance = 50,
                        Power = 8
                    },
                    new EnemyAction
                    {
                        Name = "猛撲",
                        Type = EnemyActionType.Attack,
                        Chance = 30,
                        Power = 10
                    },
                    new EnemyAction
                    {
                        Name = "低吼",
                        Type = EnemyActionType.Special,
                        Chance = 20,
                        Power = 2
                    }
                }
            };
        }

        public static Enemy CreatePoisonVineSnake()
        {
            return new Enemy
            {
                Name = "毒藤蛇",
                HP = 50,
                MaxHP = 50,
                PhysicalDefense = 2,
                MagicDefense = 4,
                CritChance = 15,

                Actions = new List<EnemyAction>
                {
                    new EnemyAction
                    {
                        Name = "毒牙",
                        Type = EnemyActionType.Attack,
                        Chance = 50,
                        Power = 10
                    },
                    new EnemyAction
                    {
                        Name = "蛇鱗護身",
                        Type = EnemyActionType.MagicDefense,
                        Chance = 25,
                        Power = 10
                    },
                    new EnemyAction
                    {
                        Name = "纏繞",
                        Type = EnemyActionType.Special,
                        Chance = 25,
                        Power = 1
                    }
                }
            };
        }

        public static Enemy CreateRockBeast()
        {
            return new Enemy
            {
                Name = "岩甲獸",
                HP = 100,
                MaxHP = 100,
                PhysicalDefense = 5,
                MagicDefense = 5,

                Actions = new List<EnemyAction>
                {
                    new EnemyAction
                    {
                        Name = "撞擊",
                        Type = EnemyActionType.Attack,
                        Chance = 35,
                        Power = 15
                    },
                    new EnemyAction
                    {
                        Name = "硬化甲殼",
                        Type = EnemyActionType.PhysicalDefense,
                        Chance = 20,
                        Power = 15
                    },
                    new EnemyAction
                    {
                        Name = "震地",
                        Type = EnemyActionType.Special,
                        Chance = 45,
                        Power = 3
                    }
                }
            };
        }

        public static Enemy CreateWindSpirit()
        {
            return new Enemy
            {
                Name = "風妖",
                HP = 80,
                MaxHP = 80,
                PhysicalDefense = 100,
                MagicDefense = -5,
                CritChance = 15,
                Actions = new List<EnemyAction>
                {
                    new EnemyAction
                    {
                        Name = "風刃",
                        Type = EnemyActionType.Attack,
                        Chance = 45,
                        Power = 10
                    },
                    new EnemyAction
                    {
                        Name = "疾風護幕",
                        Type = EnemyActionType.MagicDefense,
                        Chance = 30,
                        Power = 5
                    },
                    new EnemyAction
                    {
                        Name = "亂流",
                        Type = EnemyActionType.Special,
                        Chance = 25,
                        Power = 100
                    }
                }
            };
        }

        public static Enemy CreateDragon()
        {
            return new Enemy
            {
                Name = "龍",
                HP = 200,
                MaxHP = 200,
                PhysicalDefense = 10,
                MagicDefense = 10,
                CritChance = 20,
                Actions = new List<EnemyAction>
                {
                    new EnemyAction
                    {
                        Name = "龍爪撕裂",
                        Type = EnemyActionType.Attack,
                        Chance = 50,
                        Power = 25
                    },
                    new EnemyAction
                    {
                        Name = "龍鱗護體",
                        Type = EnemyActionType.PhysicalDefense,
                        Chance = 25,
                        Power = 40
                    },
                    new EnemyAction
                    {
                        Name = "魔力護幕",
                        Type = EnemyActionType.MagicDefense,
                        Chance = 25,
                        Power = 40
                    }
                }
            };
        }
    }
}