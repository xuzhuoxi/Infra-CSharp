using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public abstract partial class Statement
    {
        public abstract LuaValue Execute(LuaTable enviroment, out bool isBreak);
    }
}
