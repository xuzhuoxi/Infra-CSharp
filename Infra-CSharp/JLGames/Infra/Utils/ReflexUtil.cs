using System;

namespace JLGames.Infra.Utils
{
    public static class ReflexUtil
    {
        //Field----------------------------

        public static int GetFieldInt(this object obj, string name)
        {
            return Object2Int(GetField(obj, name));
        }

        public static uint GetFieldUint(this object obj, string name)
        {
            return Object2UInt(GetField(obj, name));
        }

        public static string GetFieldString(this object obj, string name)
        {
            return Object2String(GetField(obj, name));
        }

        public static bool GetFieldBool(this object obj, string name)
        {
            return Object2Bool(GetField(obj, name));
        }

        public static object GetField(this object obj, string name)
        {
            return obj.GetType().GetField(name)?.GetValue(obj);
        }

        public static T GetField<T>(this object obj, string name)
        {
            return Object2T<T>(GetField(obj, name));
        }

        //Property----------------------------

        public static int GetPropertyInt(this object obj, string name)
        {
            return Object2Int(GetProperty(obj, name));
        }

        public static uint GetPropertyUint(this object obj, string name)
        {
            return Object2UInt(GetProperty(obj, name));
        }

        public static string GetPropertyString(this object obj, string name)
        {
            return Object2String(GetProperty(obj, name));
        }

        public static bool GetPropertyBool(this object obj, string name)
        {
            return Object2Bool(GetProperty(obj, name));
        }

        public static object GetProperty(this object obj, string name)
        {
            return obj.GetType().GetProperty(name)?.GetValue(obj);
        }

        public static T GetProperty<T>(this object obj, string name)
        {
            return Object2T<T>(GetProperty(obj, name));
        }

        // ------------------------------

        private static int Object2Int(object value)
        {
            return null == value ? 0 : Convert.ToInt32(value);
        }

        private static uint Object2UInt(object value)
        {
            return null == value ? 0 : Convert.ToUInt32(value);
        }

        private static string Object2String(object value)
        {
            return value?.ToString();
        }

        private static bool Object2Bool(object value)
        {
            return null != value && Convert.ToBoolean(value);
        }

        public static T Object2T<T>(object value)
        {
            if (!(value is T)) return default(T);
            return (T) value;
        }
    }
}