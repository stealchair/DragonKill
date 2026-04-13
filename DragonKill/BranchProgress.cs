using System;
using System.Collections.Generic;
using System.Text;

namespace DragonKill
{
    //是否通過一次非龍區關卡
    public class BranchProgress
    {
        public bool LeftDownCleared { get; set; } = false;
        public bool RightDownCleared { get; set; } = false;
        public bool LeftUpCleared { get; set; } = false;
        public bool RightUpCleared { get; set; } = false;
    }
}
