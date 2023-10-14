using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Static
{
    public class DateTimeHelper
    {
        private const long startTicks = 621355968000000000;
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <param name="digit">微秒数的位数,最多7位</param>
        /// <returns></returns>
        public static long GetCurrentTimeStamp(byte digit = 0)
        {
            if (digit > 7) digit = 7;
            var length = (long)Math.Pow(10, digit);
            var _tmp = length == 1 ? 10000000 : (10000000 / (long)Math.Pow(10, digit));
            return (DateTime.UtcNow.Ticks - startTicks) / _tmp;
        }
        /// <summary>
        /// 获取当前格林时间
        ///， 时间戳小于10位时返回当前时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime GetUtcDateTime(long ticks)
        {
            var length = 0;
            var tmp_ticks = ticks;
            while (tmp_ticks > 0)
            {
                tmp_ticks /= 10;
                length++;
            }
            //var length = ticks.ToString().Length;
            return length switch
            {
                _ when length < 10 => DateTime.UtcNow,
                _ when length == 10 => new DateTime((ticks * (long)Math.Pow(10, 7)) + startTicks, DateTimeKind.Utc),
                _ => new DateTime((ticks * (long)Math.Pow(10, 17 - length)) + startTicks, DateTimeKind.Utc)
            };
        }

        /// <summary>
        /// 获取系统当前时区时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime GetLocalDateTime(long ticks)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(GetUtcDateTime(ticks), TimeZoneInfo.Local);
        }

        /// <summary>
        ///  获取时间戳Timestamp  
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long timeStamp = Convert.ToInt64((dt - dateStart).TotalSeconds);
            return timeStamp;
        }

        /// <summary>
        ///  获取时间戳Timestamp  
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dt, byte digit = 0)
        {
            dt = TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.Local);
            //dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
            if (dt.Ticks - startTicks < 0) return 0;
            if (digit > 7) digit = 7;
            var length = (long)Math.Pow(10, digit);
            var _tmp = length == 1 ? 10000000 : (10000000 / (long)Math.Pow(10, digit));
            return (dt.Ticks - startTicks) / _tmp;
        }

        public static DateTime GetDateTime(long? timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }
    }
}
