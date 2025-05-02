using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class ExprStmt : Statement
    {
        public override LuaValue Execute(LuaTable enviroment, out bool isBreak)
        {
            this.Expr.Evaluate(enviroment);
            isBreak = false;
            return null;
        }
    }
}
