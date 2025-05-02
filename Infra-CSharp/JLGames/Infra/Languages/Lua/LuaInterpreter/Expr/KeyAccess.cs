using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class KeyAccess : Access
    {
        public override LuaValue Evaluate(LuaValue baseValue, LuaTable enviroment)
        {
            LuaValue key = this.Key.Evaluate(enviroment);
            return LuaValue.GetKeyValue(baseValue, key);
        }
    }
}
