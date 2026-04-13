using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class UiService
    {
        public void ShowSlide(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
            Console.WriteLine();
            Console.WriteLine("（按任意鍵繼續）");
            Console.ReadKey(true);
        }

        public void ShowMainScreen(Player player, Inventory inventory, BranchProgress progress, LocationType currentLocation)
        {
            Console.Clear();

            Console.WriteLine("=============== 神木之巔 ===============");
            Console.WriteLine();

            DrawMap(progress, currentLocation);

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"玩家：{player.Name}");
            Console.WriteLine($"HP：{player.HP}/{player.MaxHP}");
            Console.WriteLine($"MP：{player.MP}/{player.MaxMP}");
            Console.WriteLine();
            Console.WriteLine("【背包】");
            Console.WriteLine($"劍：{(inventory.HasSword ? "O" : "X")}");
            Console.WriteLine($"法杖：{(inventory.HasStaff ? "O" : "X")}");
            Console.WriteLine($"龍心：{(inventory.HasDragonHeart ? "O" : "X")}");
            Console.WriteLine($"Key：{(inventory.HasKey ? "O" : "X")}");
            Console.WriteLine($"回血藥：x{inventory.PotionCount}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"你現在在：{GetLocationName(currentLocation)}");
            Console.WriteLine("你要往哪裡走？");
        }

        private void DrawMap(BranchProgress progress, LocationType currentLocation)
        {
            string dragon = currentLocation == LocationType.Dragon ? "○龍○" : " 龍 ";
            string leftUp = GetNodeText("左上", progress.LeftUpCleared, currentLocation == LocationType.LeftUp);
            string rightUp = GetNodeText("右上", progress.RightUpCleared, currentLocation == LocationType.RightUp);
            string leftDown = GetNodeText("左下", progress.LeftDownCleared, currentLocation == LocationType.LeftDown);
            string rightDown = GetNodeText("右下", progress.RightDownCleared, currentLocation == LocationType.RightDown);
            string root = currentLocation == LocationType.Root ? "○樹根○" : " 樹根 ";

            Console.WriteLine($"               [{dragon}]");
            Console.WriteLine();
            Console.WriteLine($"        [{leftUp}]         [{rightUp}]");
            Console.WriteLine();
            Console.WriteLine($"        [{leftDown}]       [{rightDown}]");
            Console.WriteLine();
            Console.WriteLine($"               [{root}]");
        }

        private string GetNodeText(string name, bool cleared, bool isHere)
        {
            string text = cleared ? $"{name}O" : name;

            if (isHere)
            {
                return $"○{text}○";
            }

            return text;
        }

        public string GetLocationName(LocationType location)
        {
            switch (location)
            {
                case LocationType.Root:
                    return "樹根";
                case LocationType.LeftDown:
                    return "左下";
                case LocationType.RightDown:
                    return "右下";
                case LocationType.LeftUp:
                    return "左上";
                case LocationType.RightUp:
                    return "右上";
                case LocationType.Dragon:
                    return "龍";
                default:
                    return "未知地點";
            }
        }
    }
}
