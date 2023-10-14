using Microsoft.AspNetCore.Mvc;
using Nebula.OMSV2.Client.Portal.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Template.Remote;
using Template.Static;
using Template.Web.Common;
using Template.Web.Models;

namespace Template.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PrintController : Controller
    {
        /// <summary>
        /// 收货证明打印
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<dynamic> Print(string[] codes)
        {
            //var codeList = ReadJsonCode("testcode");

            var resList = new Dictionary<string, string>();
            foreach (var item in codes)
            {
                var result = await HttpHelper.Request<OmsResponse<OrderDetailResult>>($"https://oms2api.yunexpress.com/business/orderquery/query?code={item}", default, HttpMethod.Get);
                var orderInfo = result?.Entity;
                if (orderInfo?.orderinfo != null && orderInfo?.recipient != null)
                {
                    #region 拼接参数
                    var request = new
                    {
                        fastReportTypeName = new
                        {
                            LabelName = "发货证明"
                        },
                        configInfo = new
                        {
                            LableContentType = "1",
                            LableFileType = "pdf",
                            SeparateOrMerge = "N"
                        },
                        orderInfos = new object[] {
                            new {
                                VoucherPrint = "1",
                                OrderNo = orderInfo.orderinfo?.customer_hawbcode,
                                Shipper_hawbcode = orderInfo.orderinfo?.shipper_hawbcode,
                                TrackingNo = orderInfo.orderinfo?.sever_hawbcode,
                                CustomerCode = orderInfo.orderinfo?.customer_code,
                                ForecastWeight = orderInfo.orderinfo?.order_weight.ToString("0.00"),
                                WeighingWeight = orderInfo.orderinfo?.gross_weight?.ToString(),
                                DeliveryTime = (orderInfo.orderinfo?.checkouton ?? 0) == 0 ? null : DateTimeHelper.GetUtcDateTime(orderInfo.orderinfo.checkouton.Value).ToString("yyyy-MM-dd"),
                                ConsigneeCountryCode = orderInfo.recipient?.country_code,
                                ConsigneeCountryEnName = orderInfo.orderinfo?.country_enname,
                                ConsigneeCountryCnName = orderInfo.orderinfo?.country_name,
                                ConsigneeSurname = orderInfo.recipient?.first_name,
                                ConsigneeName = orderInfo.recipient?.last_name,
                                ConsigneeCompanyName = orderInfo.recipient?.company,
                                ConsigneeAddress = orderInfo.recipient?.street1,
                                ConsigneeAddress2 = orderInfo.recipient?.street2,
                                ConsigneeAddress3 = orderInfo.recipient?.street3,
                                ConsigneeHouseNumber = orderInfo.recipient?.house_number,
                                ConsigneeCity = orderInfo.recipient?.city,
                                ConsigneeProvince = orderInfo.recipient?.province,
                                ConsigneePostCode = orderInfo.recipient?.postal_code,
                                ConsigneeTelephone = orderInfo.recipient?.telephone,
                                ConsigneePhone = orderInfo.recipient?.mobile_phone,
                                ConsigneeEmail = orderInfo.recipient?.email,
                            }
                        }
                    };
                    #endregion

                    var plsRes = await HttpHelper.Request<PlsResponse<Responses>>($"http://plsapi2oms.yunexpress.com/api/Lable/BatchReportPrint", request, HttpMethod.Post);
                    if (plsRes != null && plsRes.Success)
                    {
                        resList.Add(item, plsRes?.Data?.printInfoResponses?.FirstOrDefault()?.FileUrl ?? string.Empty);
                    }
                }
            }

            return resList;
        }
        /// <summary>
        /// 重推预报结果
        /// </summary>
        /// <returns></returns>
        [NonAction]
        [HttpPost]
        public async Task<int> PushPreResultOPS()
        {
            var path = $"{Environment.CurrentDirectory}\\Sources\\TEST.xlsx";
            var stream = FileHelper.FileToStream(path);
            var table = FileHelper.ExcelToTableFromUrl(path, stream, 0);
            int count = 0;
            var codeList = new Dictionary<string, string>();
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var req = new
                    {
                        order_id = table.Rows[i]["order_id"]?.ToString(),
                        push_type = "PredictNotify",
                        shipper_hawbcode = table.Rows[i]["shipper_hawbcode"]?.ToString(),
                        parameter = table.Rows[i]["task_id"]?.ToString()
                    };

                    var result = await HttpHelper.Request<OmsResponse<dynamic>>($"https://oms2api.yunexpress.com/business/pushcenter/AddPushTask", req, HttpMethod.Post);
                    if (result.IsSucceed == true) count++;
                }
            }
            return count;
        }
        /// <summary>
        /// 签收证明转Table
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        [NonAction]
        [HttpPost]
        public async Task<DataView> CodeToDatatable(string[] codes)
        {
            var table = new DataTable();
            var column = new List<string> { "运单号", "服务单号", "客户", "收件人", "收件人国家", "收件人城市", "收件人地址", "收件人邮编", "签收时间" };
            table.Columns.AddRange(column.Select(t => new DataColumn(t)).ToArray());
            foreach (var item in codes)
            {
                var result = await HttpHelper.Request<OmsResponse<OrderDetailResult>>($"https://oms2api.yunexpress.com/business/orderquery/query?code={item}", default, HttpMethod.Get);
                var orderInfo = result?.Entity;
                if (orderInfo?.orderinfo != null && orderInfo?.recipient != null)
                {
                    table.Rows.Add(new object[] {
                        orderInfo?.orderinfo?.shipper_hawbcode??string.Empty,
                        orderInfo?.orderinfo?.sever_hawbcode??string.Empty,
                        orderInfo?.orderinfo?.real_customername??string.Empty,
                        orderInfo?.recipient?.first_name??string.Empty,
                        orderInfo.recipient?.country_code??string.Empty,
                        orderInfo.recipient?.city ?? string.Empty,
                        ($"{orderInfo.recipient?.street1} {orderInfo.recipient?.street2 ?? string.Empty} {orderInfo.recipient?.street3 ?? string.Empty}").Trim(),
                        orderInfo.recipient?.postal_code ?? string.Empty,
                        orderInfo.orderinfo?.deliveron ?? 0L
                    });
                }
            }
            return table.AsDataView();
        }
        [NonAction]
        [HttpGet]
        public string Test()
        {
            var list = ReadJsonCode("testcode");
            return string.Join(" OR ", list);
        }

        [NonAction]
        public string[] ReadJsonCode(string fileName)
        {
            var path = $"{Environment.CurrentDirectory}\\Sources\\{fileName}.json";
            if (System.IO.File.Exists(path))
            {
                var text = System.IO.File.ReadAllText(path);
                return JsonConvert.DeserializeObject<string[]>(text);
            }
            return new string[0];
        }

        [NonAction]
        [HttpGet]
        public string GetEnum(LabelType type)
        {
            if (!Enum.IsDefined(typeof(LabelType), type))
            {
                throw new Exception("类型错误");
            }
            return type.ToString();
        }
    }
    public class OpenApiModel<T>
    {
        /// <summary>
        /// <summary>
        /// 时间戳
        /// </summary>
        /// </summary>
        public long t { get; set; } = DateTimeHelper.GetCurrentTimeStamp();
        /// <summary>
        /// <summary>
        /// 成功标识
        /// </summary>
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// 数据集
        /// </summary>
        public T result { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int? code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; }
    }
}
