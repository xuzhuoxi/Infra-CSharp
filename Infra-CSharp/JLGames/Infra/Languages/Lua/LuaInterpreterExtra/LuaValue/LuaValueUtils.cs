using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace JLGames.Infra.Languages.Lua
{
    public static class LuaValueUtils
    {
        /// <summary>
        /// Returns a LuaValue containing the object's value.
        /// 返回一个LuaValue对象，里面包含C#数据
        /// </summary>
        /// <returns>LuaValue对象(A LuaValue object)</returns>
        /// <param name="o">C#基础类型值(A value of any standard type.)</param>
        public static LuaValue ObjectToLuaValue(object o)
        {
            if (o == null) return LuaNil.Nil;
            var objectType = o.GetType();
            if (objectType == typeof(bool)) return ((bool) o) ? LuaBoolean.True : LuaBoolean.False;
            if (objectType == typeof(string)) return new LuaString((string) o);
            if (objectType == typeof(int)) return new LuaNumber((double) ((int) o));
            if (objectType == typeof(float)) return new LuaNumber((double) ((float) o));
            if (objectType == typeof(double)) return new LuaNumber((double) o);
            if (objectType == typeof(byte)) return new LuaNumber((double) ((byte) o));
            if (objectType == typeof(sbyte)) return new LuaNumber((double) ((sbyte) o));
            if (objectType == typeof(short)) return new LuaNumber((double) ((short) o));
            if (objectType == typeof(ushort)) return new LuaNumber((double) ((ushort) o));
            if (objectType == typeof(uint)) return new LuaNumber((double) ((uint) o));
            if (objectType == typeof(long)) return new LuaNumber((double) ((long) o));
            if (objectType == typeof(ulong)) return new LuaNumber((double) ((ulong) o));
            if (o is LuaValue) return (o as LuaValue);
            return new LuaString(o.ToString());
        }

        /// <summary>
        /// Returns a C# data from a LuaValue object
        /// 返回一个C#数据，数据来自LuaValue对象
        /// </summary>
        /// <param name="luaValue"></param>
        /// <returns></returns>
        public static object LuaValueToObject(LuaValue luaValue)
        {
            if (luaValue == null) return null;
            if (luaValue is LuaNumber) return (float) ((luaValue as LuaNumber).Number);
            return luaValue.Value;
        }

        /// <summary>
        /// Convert a string to a int value
        /// 把字符串转为int数值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int StringToInt(string s)
        {
            // Keep the old syntax here
            // 这里保留旧语法
            int result;
            return int.TryParse(s, out result) ? result : 0;
        }

        /// <summary>
        /// Convert a string to a float value
        /// 把字符串转为float数值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float StringToFloat(string s)
        {
            // Keep the old syntax here
            // 这里保留旧语法
            float result;
            return float.TryParse(s, System.Globalization.NumberStyles.Any
                , System.Globalization.CultureInfo.InvariantCulture, out result)
                ? result
                : 0;
        }

        /// <summary>
        /// Convert a string to a double value
        /// 把字符串转为double数值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double StringToDouble(string s)
        {
            // Keep the old syntax here
            // 这里保留旧语法
            double result;
            return double.TryParse(s, System.Globalization.NumberStyles.Any
                , System.Globalization.CultureInfo.InvariantCulture, out result)
                ? result
                : 0;
        }

        /// <summary>
        /// Returns the MethodInfo object of the Lambda function
        /// 返回Lambda函数的MethodInfo信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentException("Invalid Expression. Expression is null.");
            var outermostExpression = expression.Body as MethodCallExpression;
            if (outermostExpression == null)
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            return outermostExpression.Method;
        }

        /// <summary>
        /// Returns the MethodInfo object of the Lambda function
        /// 返回Lambda函数的MethodInfo信息
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            return GetMethodInfo((LambdaExpression) expression);
        }

        /// <summary>
        /// Returns the MethodInfo object of the Lambda function
        /// 返回Lambda函数的MethodInfo信息
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
        {
            return GetMethodInfo((LambdaExpression) expression);
        }

        /// <summary>
        /// Returns the MethodInfo object of the Lambda function
        /// 返回Lambda函数的MethodInfo信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo(Expression<Action> expression)
        {
            return GetMethodInfo((LambdaExpression) expression);
        }
    }
}