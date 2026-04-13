using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class BattleSystem
    {
        private Random random = new Random();

        public EnemyAction GetRandomEnemyAction(Enemy enemy)
        {
            int roll = random.Next(1, 101);
            int current = 0;

            foreach (EnemyAction action in enemy.Actions)
            {
                current += action.Chance;

                if (roll <= current)
                {
                    return action;
                }
            }

            return enemy.Actions[0];
        }

        public string ExecuteEnemyTurn(Player player, Enemy enemy)
        {
            enemy.IsPhysicalDefending = false;
            enemy.IsMagicDefending = false;

            EnemyAction action = GetRandomEnemyAction(enemy);

            if (action.Type == EnemyActionType.Attack)
            {
                bool isCrit = random.Next(1, 101) <= enemy.CritChance;
                int damage = action.Power;

                if (isCrit)
                {
                    damage *= 2;
                    player.HP -= damage;
                    return $"{enemy.Name} 使用了「{action.Name}」！\n發生爆擊，造成 {damage} 點傷害！";
                }
                else
                {
                    player.HP -= damage;
                    return $"{enemy.Name} 使用了「{action.Name}」！\n造成 {damage} 點傷害！";
                }
            }
            else if (action.Type == EnemyActionType.PhysicalDefense)
            {
                enemy.IsPhysicalDefending = true;
                return $"{enemy.Name} 使用了「{action.Name}」！\n進入物理防禦姿態！";
            }
            else if (action.Type == EnemyActionType.MagicDefense)
            {
                enemy.IsMagicDefending = true;
                return $"{enemy.Name} 使用了「{action.Name}」！\n進入魔法防禦姿態！";
            }
            else
            {
                enemy.TempPhysicalDefense = 0;
                enemy.TempMagicDefense = 0;

                if (enemy.Name == "樹根狼" && action.Name == "低吼")
                {
                    enemy.TempPhysicalDefense = 2;
                    enemy.TempMagicDefense = 2;
                    return "樹根狼低吼！物防+2 魔防+2";
                }

                if (enemy.Name == "毒藤蛇" && action.Name == "蛇鱗護身")
                {
                    enemy.TempPhysicalDefense = 10;
                    enemy.TempMagicDefense = -4;
                    return "毒藤蛇強化防禦！物防+10 魔防-4";
                }

                if (enemy.Name == "毒藤蛇" && action.Name == "纏繞")
                {
                    player.TempAttackBonus = -1;
                    player.TempMagicAttackBonus = -1;
                    return "你被纏繞！攻擊下降";
                }

                if (enemy.Name == "岩甲獸" && action.Name == "硬化甲殼")
                {
                    enemy.TempPhysicalDefense = 15;
                    return "岩甲獸變硬了！";
                }

                if (enemy.Name == "岩甲獸" && action.Name == "震地")
                {
                    enemy.TempPhysicalDefense = 3;
                    enemy.TempMagicDefense = 3;
                    player.HP -= 5;
                    return "震地！你受到5傷害";
                }

                if (enemy.Name == "風妖" && action.Name == "疾風護幕")
                {
                    enemy.TempMagicDefense = 5;
                    return "風妖提升魔防！";
                }

                if (enemy.Name == "風妖" && action.Name == "亂流")
                {
                    enemy.TempPhysicalDefense = -100;
                    player.HP -= 15;
                    return "亂流！你受到15傷害，防禦崩潰！";
                }

                if (enemy.Name == "龍" && action.Name == "龍鱗護體")
                {
                    enemy.TempPhysicalDefense = 40;
                    enemy.TempMagicDefense = -20;
                    return "龍強化物防！";
                }

                if (enemy.Name == "龍" && action.Name == "魔力護幕")
                {
                    enemy.TempMagicDefense = 40;
                    enemy.TempPhysicalDefense = -20;
                    return "龍強化魔防！";
                }

                return "特殊效果未定義";
            }
        }
    }
}
