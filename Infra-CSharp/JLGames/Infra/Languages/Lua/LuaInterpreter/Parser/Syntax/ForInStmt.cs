using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class ForInStmt : Statement
    {
        public List<string> NameList = new List<string>();

        public List<Expr> ExprList = new List<Expr>();

        public Chunk Body;

    }
}
