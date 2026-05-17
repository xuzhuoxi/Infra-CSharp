using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JLGames.Infra.Extensions
{
    /// <summary>
    /// String extension methods: rich text tags and regex-based splitting.
    /// 字符串扩展方法：富文本标签与基于正则的分割。
    /// </summary>
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
        /// <param name="str">Source text. 源文本。</param>
        /// <returns>Rich text wrapped with italic tags. 带斜体标签的富文本。</returns>
        public static string ToRichItalic(this string str)
        {
            return $"{ItalicTag}{str}{ItalicTagEnd}";
        }

        /// <summary>
        /// Convert to bold rich text
        /// 转化为粗体富文本
        /// </summary>
        /// <param name="str">Source text. 源文本。</param>
        /// <returns>Rich text wrapped with bold tags. 带粗体标签的富文本。</returns>
        public static string ToRichBold(this string str)
        {
            return $"{BoldTag}{str}{BoldTagEnd}";
        }

        /// <summary>
        /// Convert to rich text with text size
        /// 转化为带文本大小富文本
        /// </summary>
        /// <param name="str">Source text. 源文本。</param>
        /// <param name="size">Font size. 字体大小。</param>
        /// <returns>Rich text wrapped with size tag. 带字号标签的富文本。</returns>
        public static string ToRichSize(this string str, int size)
        {
            return $"<size={size}>{str}{SizeTagEnd}";
        }


        /// <summary>
        /// Convert to rich text with text size
        /// 转化为带文本大小富文本
        /// </summary>
        /// <param name="str">Source text. 源文本。</param>
        /// <param name="size">Font size value or unit string. 字号数值或单位字符串。</param>
        /// <returns>Rich text wrapped with size tag. 带字号标签的富文本。</returns>
        public static string ToRichSize(this string str, string size)
        {
            return $"<size={size}>{str}{SizeTagEnd}";
        }

        /// <summary>
        /// Convert to rich text with text color
        /// 转化为带文本颜色富文本
        /// </summary>
        /// <param name="str">Source text. 源文本。</param>
        /// <param name="color">Color as #RRGGBB, hex without #, or color name. 颜色（#RRGGBB、无 # 的十六进制或颜色名）。</param>
        /// <returns>Rich text wrapped with color tag. 带颜色标签的富文本。</returns>
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
        /// <param name="matched">Matched substring. 匹配到的子串。</param>
        /// <returns>Transformed string to include in split result. 纳入分割结果中的转换后字符串。</returns>
        public delegate string MatchedAction(string matched);

        /// <summary>
        /// Split string using regular expression
        /// 使用正则表达式分割字符串
        /// </summary>
        /// <param name="str">Source string. 源字符串。</param>
        /// <param name="regex">Pattern used as delimiters. 用作分隔符的正则表达式。</param>
        /// <param name="includeMatched">Whether matched segments are included in the result. 是否将匹配段纳入结果。</param>
        /// <param name="matchedAction">Transform for matched segments when <paramref name="includeMatched"/> is true; ignored otherwise. <paramref name="includeMatched"/> 为 true 时对匹配段的转换；否则忽略。</param>
        /// <returns>Split segments; null when <paramref name="str"/> is null or empty. 分割后的片段；<paramref name="str"/> 为空时返回 null。</returns>
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
