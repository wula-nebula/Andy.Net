using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.OrderApp.DTO
{
    public class OrderExt
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public long order_id { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string order_code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
