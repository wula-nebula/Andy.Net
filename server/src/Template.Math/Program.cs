using System;
using System.Text.RegularExpressions;

namespace Template.Math
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Console.WriteLine(findTheWinner(10,3));

            //var aa = Regex.IsMatch(string.Empty, @"^0$|^0.\d{1}$|^1$");
            //string[] ss = { "aa","bb","cc"};
            //int[] aa = { 1, 2, 3, 4, 5 };
            //Console.WriteLine(Array.IndexOf(aa, 3));
            //Console.WriteLine(TimeZoneInfo.Local.BaseUtcOffset.Hours);

            var result = (70.06 * 1).ToString("0.00");
                Console.WriteLine(result);
            //var rgx = new Regex("^.+_D\\d{3,4}$");
            //var str = "0B052024500007BBD8044_D123";
            //if (rgx.IsMatch(str))//还原跟踪号
            //{
            //    Console.WriteLine(rgx.Match(str).Groups[1].Value);
            //}
            
            Console.ReadKey();
        }
        static int findTheWinner(int n, int k)
        {
            int pos = 0;
            for (int i = 2; i < n + 1; ++i)
            {
                pos = (pos + k) % i;
            }
            return pos + 1;
        }
    }
}
