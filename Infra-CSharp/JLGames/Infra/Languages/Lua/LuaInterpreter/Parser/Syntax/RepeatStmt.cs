using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class RepeatStmt : Statement
    {
        public Chunk Body;

        public Expr Condition;

    }
}
