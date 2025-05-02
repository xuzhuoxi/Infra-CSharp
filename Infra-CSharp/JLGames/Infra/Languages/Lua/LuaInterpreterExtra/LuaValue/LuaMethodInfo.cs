using System;
using System.Reflection;

namespace JLGames.Infra.Languages.Lua
{
    /// <summary>
    /// C# function wrapper class for registering in Lua
    /// C#函数包装类，用于注册到Lua中去
    /// </summary>
    public class LuaMethodInfo : LuaFunction
    {
        private readonly object m_Target;
        private readonly MethodInfo m_Method;

        public object Target => m_Target;
        public MethodInfo Method => m_Method;

        public LuaMethodInfo(object target, MethodInfo method) : base(null)
        {
            Function = InvokeMethod;
            m_Target = target;
            m_Method = method;
        }

        public LuaValue InvokeMethod(LuaValue[] args)
        {
            if (Method == null || args == null) return LuaNil.Nil;
            var objectArgs = new object[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                objectArgs[i] = LuaValueUtils.LuaValueToObject(args[i]);
            }

            try
            {
                var result = Method.Invoke(Target, objectArgs);
                return LuaValueUtils.ObjectToLuaValue(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}