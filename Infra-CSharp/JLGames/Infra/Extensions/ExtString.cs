using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JLGames.Infra.Extensions
{
    public static class ExtString
    {
        /// <summary>
        /// Tag with rich text bold
        /// 富文本粗体的标签
        /// </summary>
        public const string BoldTag = "<b>";

        /// <summary>
        /// End tag with rich text bold
        /// 富文本粗体的结束标签
        /// </summary>
        public const string BoldTagEnd = "</b>";

        /// <summary>
        /// Tag with rich text italic
        /// 富文本斜体的标签
        /// </summary>
        public const string ItalicTag = "<i>";

        /// <summary>
        /// End tag with rich text italic
        /// 富文本斜体的结束标签
        /// </summary>
        public const string ItalicTagEnd = "</i>";

        /// <summary>
        /// End tag with rich text font size
        /// 富文本字体大小的结束标签
        /// </summary>
        public const string SizeTagEnd = "</size>";

        /// <summary>
        /// End tag with rich text font color
        /// 富文本字体颜色的结束标签
        /// </summary>
        public const string ColorTagEnd = "</color>";

        /// <summary>
        /// Convert to italic rich text
        /// 转化为斜体富文本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToRichItalic(this string str)
        {
            return $"{ItalicTag}{str}{ItalicTagEnd}";
        }

        /// <summary>
        /// Convert to bold rich text
        /// 转化为粗体富文本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToRichBold(this string str)
        {
            return $"{BoldTag}{str}{BoldTagEnd}";
        }

        /// <summary>
        /// Convert to rich text with text size
        /// 转化为带文本大小富文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToRichSize(this string str, int size)
        {
            return $"<size={size}>{str}{SizeTagEnd}";
        }


        /// <summary>
        /// Convert to rich text with text size
        /// 转化为带文本大小富文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToRichSize(this string str, string size)
        {
            return $"<size={size}>{str}{SizeTagEnd}";
        }

        /// <summary>
        /// Convert to rich text with text color
        /// 转化为带文本颜色富文本
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToRichColor(this string str, string color)
        {
            if (color.StartsWith("#"))
                return $"<color={color}>{str}{ColorTagEnd}";

            try
            {
                var temp = Convert.ToUInt64(color, 16);
                return $"<color=#{color}>{str}{ColorTagEnd}";
            }
            catch (Exception e)
            {
                var _ = e;
                return $"<color={color}>{str}{ColorTagEnd}";
            }
        }

        /// <summary>
        /// Processing behavior when regular matching
        /// 正则匹配时的处理行为
        /// </summary>
        /// <param name="matched"></param>
        public delegate string MatchedAction(string matched);

        /// <summary>
        /// Split string using regular expression
        /// 使用正则表达式分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regex"></param>
        /// <param name="includeMatched"></param>
        /// <param name="matchedAction">ignore when includeMatched=false</param>
        /// <returns></returns>
        public static string[] Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)
        {
            if (string.IsNullOrEmpty(str)) return null;
            if (null == regex) return new[] {str};
            var list = new List<string>();
            var mc = regex.Matches(str);
            var pos = 0;
            foreach (Match match in mc)
            {
                var mIndex = match.Index;
                if (mIndex != pos)
                {
                    list.Add(str.Substring(pos, mIndex - pos));
                }

                pos = mIndex + match.Length;
                if (includeMatched)
                {
                    if (null == matchedAction)
                        list.Add(match.Value);
                    else
                        list.Add(matchedAction.Invoke(match.Value));
                }
            }

            if (str.Length > pos) list.Add(str.Substring(pos));
            return list.ToArray();
        }
    }
}