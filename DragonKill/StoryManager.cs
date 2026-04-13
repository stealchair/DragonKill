using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    public class StoryManager//劇情
    {
        public void PlayFirstEnter(LocationType location, UiService ui)
        {
            switch (location)
            {
                case LocationType.LeftDown:
                    ui.ShowSlide("你踏入潮濕的樹根之間...");
                    ui.ShowSlide("黑暗中，一雙眼睛盯著你...");
                    ui.ShowSlide("樹根狼現身！");
                    break;

                case LocationType.RightDown:
                    ui.ShowSlide("藤蔓纏繞你的腳...");
                    ui.ShowSlide("一條毒藤蛇從陰影中滑出...");
                    break;

                case LocationType.LeftUp:
                    ui.ShowSlide("地面震動...");
                    ui.ShowSlide("岩甲獸從岩石中甦醒...");
                    break;

                case LocationType.RightUp:
                    ui.ShowSlide("風開始旋轉...");
                    ui.ShowSlide("風妖在空中凝聚...");
                    break;

                case LocationType.Dragon:
                    ui.ShowSlide("天空變暗...");
                    ui.ShowSlide("巨龍從雲中降臨...");
                    break;
            }
        }
    }
}