namespace JLGames.Infra.Languages.Lua
{
    /// <summary>
    /// LuaTable extension
    /// LuaTable行为扩展
    /// </summary>
    public static class LuaTableExtension
    {
        /// <summary>
        /// Register the C# method through the MethodInfo object.
        /// Registers a C# method by its MethodInfo.
        /// 通过MethodInfo对象注册C#方法。
        /// </summary>
        /// <param name="luaTable"></param>
        /// <param name="funcName">注册名称，Lua脚本中通过注册名称调用C#函数。（Register the name, call the C# function through the registered name in the Lua script.）</param>
        /// <param name="target">C#函数的执行对象(The execution object of the C# function)</param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static LuaFunction RegisterMethodFunction(this LuaTable luaTable, string funcName, object target,
            System.Reflection.MethodInfo methodInfo)
        {
            var luaFunc = new LuaMethodInfo(target, methodInfo);
            luaTable.SetNameValue(funcName, luaFunc);
            return luaFunc;
        }

        /// <summary>
        /// Reset
        /// 重置
        /// </summary>
        /// <param name="luaTable"></param>
        public static void Reset(this LuaTable luaTable)
        {
            if (luaTable.Length > 0)
            {
                for (var index = luaTable.Length - 1; index >= 0; index--)
                {
                    luaTable.RemoveAt(index);
                }
            }

            if (luaTable.Count > 0)
            {
                var keys = new LuaValue[luaTable.Count];
                var index = 0;
                foreach (var key in luaTable.Keys)
                {
                    keys[index] = key;
                    index += 1;
                }

                foreach (var key in keys)
                {
                    luaTable.SetKeyValue(key, LuaNil.Nil);
                }
            }

            luaTable.MetaTable = null;
        }
    }
}