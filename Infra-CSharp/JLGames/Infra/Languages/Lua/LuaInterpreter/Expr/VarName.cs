using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class VarName : BaseExpr
    {
        public override LuaValue Evaluate(LuaTable enviroment)
        {
            return enviroment.GetValue(this.Name);
        }

        public override Term Simplify()
        {
            return this;
        }
    }
}
