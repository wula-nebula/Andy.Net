using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Elastic
{
    public class EsSearchResponce
    {
        /// <summary>
        /// 条数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object item { get; set; }
    }
}
