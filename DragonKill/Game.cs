using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class Game
    {
        //遊戲持續運行
        private bool isRunning = true;
        //調用所有class
        private Player player = new Player();
        private Inventory inventory = new Inventory();
        private BranchProgress progress = new BranchProgress();
        private BattleSystem battleSystem = new BattleSystem();
        private LocationType currentLocation = LocationType.Root;
        private StoryManager story = new StoryManager();
        private UiService ui = new UiService();
        //怪物是房間指定，非新建


        public void Start()
        {
            ShowIntro();
            AskPlayerName();
            MainLoop();
        }

        private void ShowIntro()
        {
            ui.ShowSlide("很久以前，神木之巔棲息著一條巨龍。");
            ui.ShowSlide("凡是想挑戰牠的人，都必須穿越神木中的試煉。");
            ui.ShowSlide("有人為了力量而來，有人為了真相而來。");
            ui.ShowSlide("而你，也踏上了這條通往樹頂的道路。");
            ui.ShowSlide("當你抬起頭時，巨大的樹影早已遮蔽天空。");
        }

        private void AskPlayerName()
        {
            Console.Clear();
            Console.WriteLine("請輸入你的名字：");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                player.Name = "無名旅人";
            }
            else
            {
                player.Name = input.Trim();
            }
        }

        private void MainLoop()
        {
            while (isRunning)
            {
                ui.ShowMainScreen(player, inventory, progress, currentLocation);
                ShowMovementOptions();

                string? input = Console.ReadLine();
                HandleMovementInput(input);
            }
        }

        //顯示可走路線，有分支、不能亂走
        private void ShowMovementOptions()
        {
            List<LocationType> nextLocations = GetAvailableLocations();

            for (int i = 0; i < nextLocations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ui.GetLocationName(nextLocations[i])}");
            }

            Console.WriteLine("0. 離開遊戲");
        }

        //現在具體位置
        private List<LocationType> GetAvailableLocations()
        {
            List<LocationType> locations = new List<LocationType>();

            switch (currentLocation)
            {
                case LocationType.Root:
                    locations.Add(LocationType.LeftDown);
                    locations.Add(LocationType.RightDown);
                    break;

                case LocationType.LeftDown:
                    locations.Add(LocationType.Root);
                    locations.Add(LocationType.LeftUp);
                    locations.Add(LocationType.RightUp);
                    break;

                case LocationType.RightDown:
                    locations.Add(LocationType.Root);
                    locations.Add(LocationType.LeftUp);
                    locations.Add(LocationType.RightUp);
                    break;

                case LocationType.LeftUp:
                    locations.Add(LocationType.LeftDown);
                    locations.Add(LocationType.RightDown);
                    locations.Add(LocationType.Dragon);
                    break;

                case LocationType.RightUp:
                    locations.Add(LocationType.LeftDown);
                    locations.Add(LocationType.RightDown);
                    locations.Add(LocationType.Dragon);
                    break;

                case LocationType.Dragon:
                    locations.Add(LocationType.LeftUp);
                    locations.Add(LocationType.RightUp);
                    break;
            }

            return locations;
        }

        //如果是輸入0，則退出遊戲，根據其他可以走的選項走
        private void HandleMovementInput(string? input)
        {
            if (input == "0")
            {
                isRunning = false;
                return;
            }

            List<LocationType> nextLocations = GetAvailableLocations();

            if (!int.TryParse(input, out int choice))
            {
                ui.ShowSlide("請輸入正確選項。");
                return;
            }

            if (choice < 1 || choice > nextLocations.Count)
            {
                ui.ShowSlide("沒有這個選項。");
                return;
            }

            LocationType selectedLocation = nextLocations[choice - 1];

            if (selectedLocation == LocationType.Dragon)
            {
                if (!inventory.HasKey || !inventory.HasDragonHeart)
                {
                    ui.ShowSlide("你來到龍前的道路，但門沒有回應。\n需要 Key 與 龍心 才能前進。");
                    return;
                }
            }

            currentLocation = selectedLocation;
            OnEnterLocation();
        }

        //戰鬥結算
        private void OnEnterLocation()
        {
            switch (currentLocation)
            {
                case LocationType.Root:
                    return;

                case LocationType.LeftDown:
                    story.PlayFirstEnter(currentLocation, ui);

                    HandleRoom(
                        EnemyFactory.CreateRootWolf(),
                        progress.LeftDownCleared,
                        () => progress.LeftDownCleared = true,
                        () => inventory.HasSword = true,
                        "你獲得了 劍！"
                    );
                    break;

                case LocationType.RightDown:
                    story.PlayFirstEnter(currentLocation, ui);

                    HandleRoom(
                        EnemyFactory.CreatePoisonVineSnake(),
                        progress.RightDownCleared,
                        () => progress.RightDownCleared = true,
                        () => inventory.HasStaff = true,
                        "你獲得了 法杖！"
                    );
                    break;

                case LocationType.LeftUp:
                    story.PlayFirstEnter(currentLocation, ui);

                    HandleRoom(
                        EnemyFactory.CreateRockBeast(),
                        progress.LeftUpCleared,
                        () => progress.LeftUpCleared = true,
                        () => inventory.HasKey = true,
                        "你獲得了 Key！"
                    );
                    break;

                case LocationType.RightUp:
                    story.PlayFirstEnter(currentLocation, ui);

                    HandleRoom(
                        EnemyFactory.CreateWindSpirit(),
                        progress.RightUpCleared,
                        () => progress.RightUpCleared = true,
                        () => inventory.HasDragonHeart = true,
                        "你獲得了 龍心！"
                    );
                    break;

                case LocationType.Dragon:
                    story.PlayFirstEnter(currentLocation, ui);

                    HandleDragon();
                    break;
            }
        }

        //下層四個房間狀態顯示
        private void HandleRoom(
            Enemy enemy,
            bool isCleared,
            Action markCleared,
            Action giveReward,
            string rewardText)
        {
            if (isCleared)
            {
                ui.ShowSlide("這裡的敵人已經被擊敗過，但仍有殘存的生物……");
            }
            else
            {
                ui.ShowSlide($"你遇到了 {enemy.Name}！");
            }

            bool win = StartBattle(enemy);

            if (!win)
            {
                ui.ShowSlide("你被擊敗了……");
                isRunning = false;
                return;
            }

            inventory.PotionCount += 2;

            if (!isCleared)
            {
                giveReward();
                markCleared();

                ui.ShowSlide($"你擊敗了 {enemy.Name}！\n{rewardText}\n獲得：回血藥 x2");
            }
            else
            {
                ui.ShowSlide($"你擊敗了 {enemy.Name}！\n獲得：回血藥 x2");
            }
        }

        //戰鬥開始入口
        private bool StartBattle(Enemy enemy)
        {
            while (player.HP > 0 && enemy.HP > 0)
            {
                ui.ShowSlide($"【戰鬥】\n你 HP：{player.HP}\n{enemy.Name} HP：{enemy.HP}");

                Console.WriteLine("你的回合：");
                Console.WriteLine("1. 拳頭");
                Console.WriteLine("2. 揮劍");
                Console.WriteLine("3. 法杖");
                Console.WriteLine("4. 使用回血藥");

                string input = Console.ReadLine();

                int damage = 0;
                int baseAttack = 0;
                bool isMagic = false;

                // 行動判定
                if (input == "1")
                {
                    baseAttack = player.Attack;
                }
                else if (input == "2")
                {
                    if (!inventory.HasSword)
                    {
                        ui.ShowSlide("你沒有劍！");
                        continue;
                    }
                    baseAttack = player.Attack + 7;
                }
                else if (input == "3")
                {
                    if (!inventory.HasStaff)
                    {
                        ui.ShowSlide("你沒有法杖！");
                        continue;
                    }

                    if (player.MP < 15)
                    {
                        ui.ShowSlide("MP 不足！");
                        continue;
                    }

                    player.MP -= 15;

                    baseAttack = player.MagicAttack;
                    isMagic = true;
                }
                else if (input == "4")
                {
                    if (inventory.PotionCount <= 0)
                    {
                        ui.ShowSlide("你沒有藥！");
                        continue;
                    }

                    inventory.PotionCount--;
                    player.HP = Math.Min(player.MaxHP, player.HP + 30);
                    player.MP = Math.Min(player.MaxMP, player.MP + 25);

                    ui.ShowSlide("你使用了藥水！\nHP +30\nMP +25");
                    continue;
                }

                //攻擊傷害計算
                Random rand = new Random();

                // 英雄之拳、拳頭有1%機率爆擊
                if (input == "1" && rand.Next(100) < 1)
                {
                    damage = 1000;
                    ui.ShowSlide(" 英雄之拳！造成 1000 傷害！");
                }
                else
                {
                    bool isCrit = rand.Next(100) < 30;

                    if (isCrit)
                    {
                        baseAttack *= 2;
                        ui.ShowSlide(" 爆擊！");
                    }

                    if (isMagic)
                    {
                        int finalMagicAttack = baseAttack + player.TempMagicAttackBonus;
                        int finalMagicDefense = enemy.MagicDefense + enemy.TempMagicDefense;

                        damage = finalMagicAttack - finalMagicDefense;
                    }
                    else
                    {
                        int finalPhysicalAttack = baseAttack + player.TempAttackBonus;
                        int finalPhysicalDefense = enemy.PhysicalDefense + enemy.TempPhysicalDefense;

                        // 風妖特殊規則，平常只能法杖打，除非這回合亂流
                        if (enemy.Name == "風妖" && enemy.TempPhysicalDefense > -100)
                        {
                            damage = 0;
                        }
                        else
                        {
                            damage = finalPhysicalAttack - finalPhysicalDefense;
                        }
                    }

                    if (damage < 0)
                        damage = 0;
                }

                enemy.HP -= damage;
                ui.ShowSlide($"你造成 {damage} 點傷害！");

                player.TempAttackBonus = 0;
                player.TempMagicAttackBonus = 0;

                enemy.TempPhysicalDefense = 0;
                enemy.TempMagicDefense = 0;

                if (enemy.HP <= 0)
                    break;

                string result = battleSystem.ExecuteEnemyTurn(player, enemy);
                ui.ShowSlide(result);
            }

            return player.HP > 0;
        }

        //到龍面前
        private void HandleDragon()
        {
            ui.ShowSlide("你面前站著那條傳說中的龍……");

            Enemy dragon = EnemyFactory.CreateDragon();

            bool win = StartBattle(dragon);

            if (!win)
            {
                ui.ShowSlide("你被龍擊敗了……");
                isRunning = false;
                return;
            }

            ui.ShowSlide("你擊敗了龍！你完成了試煉！");
            isRunning = false;
        }
    }
}