using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Template.HealthCheck
{
    /// <summary>
    /// 提供一些日期和时间的帮助方法
    /// </summary>
    public static class DateTimeHelper
    {

        private static readonly Regex timeSpanRegex = new Regex(@"^\s*(?<value>[0-9]+(\.[0-9]+)?)\s*(?<unit>ms|s|m|h|d)\s*$");


        /// <summary>
        /// 解析时间段值
        /// </summary>
        /// <param name="text">要解析为时间段的文本</param>
        /// <returns>TimeSpan 类型值</returns>
        public static TimeSpan ParseTimeSpan(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (double.TryParse(text, out var number))
                return TimeSpan.FromMilliseconds(number);

            if (text.Equals("Infinite", StringComparison.OrdinalIgnoreCase))
                return Timeout.InfiniteTimeSpan;


            var match = timeSpanRegex.Match(text);
            if (match.Success)
            {
                var value = double.Parse(match.Groups["value"].Value);
                var unit = match.Groups["unit"].Value;

                switch (unit)
                {
                    case "ms":
                        return TimeSpan.FromMilliseconds(value);

                    case "s":
                        return TimeSpan.FromSeconds(value);

                    case "m":
                        return TimeSpan.FromMinutes(value);

                    case "h":
                        return TimeSpan.FromHours(value);

                    case "d":
                        return TimeSpan.FromDays(value);

                    default:
                        break;
                }
            }


            return TimeSpan.Parse(text);
        }


        /// <summary>
        /// 获取配置数据中的时间段值
        /// </summary>
        /// <param name="configuration">配置信息</param>
        /// <param name="key">配置键</param>
        /// <returns>TimeSpan 类型值</returns>
        public static TimeSpan TimeSpanValue(this IConfiguration configuration, string key)
        {
            return ParseTimeSpan(configuration[key]);
        }

        /// <summary>
        /// 获取配置数据中的时间段值
        /// </summary>
        /// <param name="configuration">配置信息</param>
        /// <param name="key">配置键</param>
        /// <param name="defaultValue">要使用的默认值</param>
        /// <returns>TimeSpan 类型值</returns>
        public static TimeSpan TimeSpanValue(this IConfiguration configuration, string key, TimeSpan defaultValue)
        {
            var value = configuration[key];
            if (value != null)
                return ParseTimeSpan(value);

            else
                return defaultValue;
        }


    }
}
