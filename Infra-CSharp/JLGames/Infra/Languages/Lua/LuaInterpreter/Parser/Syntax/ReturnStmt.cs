using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class ReturnStmt : Statement
    {
        public List<Expr> ExprList = new List<Expr>();

    }
}
