using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class WhileStmt : Statement
    {
        public Expr Condition;

        public Chunk Body;

    }
}
