using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Languages.Lua
{
    public partial class PrimaryExpr : Term
    {
        public BaseExpr Base;

        public List<Access> Accesses = new List<Access>();

    }
}
