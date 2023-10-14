using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Web
{
    public class UserSource: UserModel
    {
        public string name { get; set; }
        public long? age { get; set; }
        public decimal? money { get; set; }
        public string address { get; set; }
        public DateTime? createon { get; set; }
    }
}
