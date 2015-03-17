using System;
using System.Text.RegularExpressions;

namespace ZuckLibrary.Utils
{
    public class RegexUtil
    {
        private static TimeSpan _defaultTimeout = TimeSpan.FromSeconds(2);

        /// <summary>
        /// 添加了超时保护的正则表达式匹配，默认超时时间为2秒，默认匹配模式为None
        /// </summary>
        /// <param name="input">需要验证的字符串</param>
        /// <param name="pattern">用作匹配的正则表达式</param>
        /// <returns>返回一个Match结构，匹配失败或超时则返回Match.Empty</returns>
        public static Match SafeMatch(string input, string pattern)
        {
            return SafeMatch(input, pattern, RegexOptions.None, _defaultTimeout);
        }

        /// <summary>
        /// 添加了超时保护的正则表达式匹配，默认超时时间为2秒，默认匹配模式为None
        /// </summary>
        /// <param name="input">需要验证的字符串</param>
        /// <param name="pattern">用作匹配的正则表达式</param>
        /// <param name="option">匹配模式</param>
        /// <returns>返回一个Match结构，匹配失败或超时则返回Match.Empty</returns>
        public static Match SafeMatch(string input, string pattern, RegexOptions option)
        {
            return SafeMatch(input, pattern, option, _defaultTimeout);
        }

        /// <summary>
        /// 添加了超时保护的正则表达式匹配，默认超时时间为2秒，默认匹配模式为None
        /// </summary>
        /// <param name="input">需要验证的字符串</param>
        /// <param name="pattern">用作匹配的正则表达式</param>
        /// <param name="option">匹配模式</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回一个Match结构，匹配失败或超时则返回Match.Empty</returns>
        public static Match SafeMatch(string input, string pattern, RegexOptions option, TimeSpan timeout)
        {
            if (timeout != TimeSpan.MinValue)
            {
                try
                {
                    Match result = Regex.Match(input, pattern, option, timeout);
                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Error("Regex Test Failed", pattern);
                    Logger.Error(ex.Message, ex.StackTrace);
                }
            }

            return Match.Empty;
        }

        /// <summary>
        /// 正则表达式测试，使用Uri的Host减少字符串匹配
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool RegexTest(string url, string pattern)
        {
            if (String.IsNullOrEmpty(url) || String.IsNullOrEmpty(pattern))
                return false;

            Uri tmpUri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out tmpUri)) return false;

            try
            {
                return Regex.IsMatch(tmpUri.Host, pattern, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2));
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e.StackTrace);
                return false;
            }
        }
    }
}