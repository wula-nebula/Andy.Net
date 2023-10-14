using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Web.Models
{
    public class OmsResponse<T>
    {
        public int StatusCode { get; set; }
        public string MsgCode { get; set; }
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T Entity { get; set; }
    }
    /// <summary>
    /// 信息
    /// </summary>
    public class OrderDetailResult
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public SearchOrderResult orderinfo { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public OdrDetailsReceiverAddress recipient { get; set; }
    }
    /// <summary>
    /// 结果对象
    /// </summary>
    public class SearchOrderResult
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public long order_id { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public long createon { get; set; }
        /// <summary>
        /// 系统来源
        /// </summary>
        public string system_source { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        public string body_code { get; set; }
        /// <summary>
        /// 业务员ID
        /// </summary>
        public int? salesman_id { get; set; }
        /// <summary>
        /// 业务员code
        /// </summary>
        public string salesman_code { get; set; }
        /// <summary>
        /// 业务员名称
        /// </summary>
        public string salesman_name { get; set; }
        /// <summary>
        /// 客服id
        /// </summary>
        public int? customer_service_id { get; set; }

        /// <summary>
        /// 客服code
        /// </summary>
        public string customer_service_code { get; set; }
        /// <summary>
        /// 客服名称
        /// </summary>
        public string customer_service_name { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public string source_code { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int? audit_status { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int customer_id { get; set; }
        /// <summary>
        /// 客户code
        /// </summary>
        public string customer_code { get; set; }
        /// <summary>
        /// 客户简称
        /// </summary>
        public string customer_shortname { get; set; }
        /// <summary>
        /// 客户全称
        /// </summary>
        public string customer_allname { get; set; }

        /// <summary>
        /// 客户组织机构
        /// </summary>
        public int customer_og_id { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string shipper_hawbcode { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public string customer_hawbcode { get; set; }
        /// <summary>
        /// 服务单号(跟踪号)
        /// </summary>
        public string sever_hawbcode { get; set; }
        /// <summary>
        /// 平台单号
        /// </summary>
        public string platform_hawbcode { get; set; }
        /// <summary>
        /// 销售产品code
        /// </summary>
        public string product_code { get; set; }
        /// <summary>
        /// 销售产品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 服务商code
        /// </summary>
        public string service_code { get; set; }
        /// <summary>
        /// 服务商名称
        /// </summary>
        public string service_name { get; set; }

        /// <summary>
        /// 服务渠道id
        /// </summary>
        public int? server_channelid { get; set; }

        /// <summary>
        /// 渠道id
        /// </summary>
        public int? channel_id { get; set; }

        /// <summary>
        /// 渠道code
        /// </summary>
        public string channel_code { get; set; }
        /// <summary>
        /// 渠道名称
        /// </summary>
        public string channel_name { get; set; }

        /// <summary>
        /// 国家二字简码
        /// </summary>
        public string country_code { get; set; }
        /// <summary>
        /// 国家名称
        /// </summary>
        public string country_name { get; set; }
        /// <summary>
        /// 国家英文名称
        /// </summary>
        public string country_enname { get; set; }
        /// <summary>
        /// 件数 1票1件，1票多件
        /// </summary>
        public int order_pieces { get; set; }
        /// <summary>
        /// 订单重
        /// </summary>
        public decimal order_weight { get; set; }
        /// <summary>
        /// 是否拦截
        /// </summary>
        public string intercept_status { get; set; }
        /// <summary>
        /// 拦截备注
        /// </summary>
        public string intercept_note { get; set; }
        /// <summary>
        /// 拦截人
        /// </summary>
        public int? Interceptor_id { get; set; }
        /// <summary>
        /// 拦截人备注
        /// </summary>
        public string Interceptor_code { get; set; }
        /// <summary>
        /// 拦截人姓名
        /// </summary>
        public string Interceptor_name { get; set; }

        /// <summary>
        /// 预报时间
        /// </summary>
        public long? submitedon { get; set; }
        /// <summary>
        /// 签入时间
        /// </summary>
        public long? checkinon { get; set; }
        /// <summary>
        /// 签出时间
        /// </summary>
        public long? checkouton { get; set; }
        /// <summary>
        /// 签收时间
        /// </summary>
        public long? deliveron { get; set; }
        /// <summary>
        /// 理赔时间
        /// </summary>
        public long? claimson { get; set; }
        /// <summary>
        /// 邮寄货物类型
        /// </summary>
        public int? mail_cargo_type { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        public decimal? length { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public decimal? width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        public decimal? height { get; set; }
        /// <summary>
        /// 收货网点
        /// </summary>
        public string receipt_node { get; set; }

        /// <summary>
        /// 增值税号
        /// </summary>
        public string vat_number { get; set; }
        /// <summary>
        /// 登记号
        /// </summary>
        public string eori_number { get; set; }
        /// <summary>
        /// ioss_code
        /// </summary>
        public string ioss_code { get; set; }
        /// <summary>
        /// 是否云途预缴
        /// </summary>
        public bool? is_ytioss { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public string weight_units { get; set; }

        /// <summary>
        /// 关键字拦截状态 0正常 1待审核 2已审核 3审核失败
        /// </summary>
        public int keyword_intercept_state { get; set; }

        /// <summary>
        /// 装箱单URL
        /// </summary>
        public string packing_list_url { get; set; }

        /// <summary>
        /// 服务商单号换号类型
        /// </summary>
        public int document_change_sign { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string order_note { get; set; }

        /// <summary>
        /// 退件标记
        /// 是否退回,包裹无人签收时是否退回，1-退回，0-不退回，默认0
        /// </summary>
        public string return_sign { get; set; }
        /// <summary>
        /// 退件类型
        /// </summary>
        public string return_type { get; set; }
        /// <summary>
        /// 退件原因
        /// </summary>
        public string return_reason { get; set; }
        /// <summary>
        /// 是否需要校验偏远地址
        /// </summary>
        public string faraway_sign { get; set; }
        /// <summary>
        /// 是否偏远地址
        /// </summary>
        public string is_faraway { get; set; }
        /// <summary>
        /// 打印状态
        /// </summary>
        public string print_sign { get; set; }
        /// <summary>
        /// 打印时间
        /// </summary>
        public long? printedon { get; set; }

        /// <summary>
        /// 包裹投保类型，0-不参保，1-按件，2-按比例，默认0，表示不参加运输保险，具体参考包裹运输
        /// </summary>
        public int? insurance_type { get; set; }

        /// <summary>
        /// 保险的最高额度，单位RMB
        /// </summary>
        public decimal? insure_amount { get; set; }

        /// <summary>
        /// 申报类型，用于打印CN22，1-Gift,2-Sameple,3-Documents,4-Others,默认4-Other
        /// </summary>
        public int? application_type { get; set; }

        /// <summary>
        /// 包裹中特殊货品类型，可调用货品类型查询服务查询，可以不填写，表示普通货品
        /// </summary>
        public int? sensitive_type { get; set; }

        /// <summary>
        /// 称重重量
        /// </summary>
        public decimal? gross_weight { get; set; }
        /// <summary>
        /// 计费重量
        /// </summary>
        public decimal? charge_weight { get; set; }
        /// <summary>
        /// 材积重量
        /// </summary>
        public decimal? volume_weight { get; set; }
        /// <summary>
        /// 真实客户code
        /// </summary>
        public string real_customercode { get; set; }
        /// <summary>
        /// 真实客户名称
        /// </summary>
        public string real_customername { get; set; }
        /// <summary>
        /// 计费金额
        /// </summary>
        public decimal? cost_fee { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string order_type { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime last_updated_on { get; set; }

        /// <summary>
        /// 面单条形码集合
        /// </summary>
        public string barcodes { get; set; }

        /// <summary>
        /// 打印格式1：PDF 2：ZPL 3：PNG
        /// </summary>
        public int label_type { get; set; }
        /// <summary>
        /// 是否报关件
        /// </summary>
        public bool is_customerdeclare { get; set; }

        /// <summary>
        /// 是否关税预付
        /// </summary>
        public bool is_tariff_prepaid { get; set; }

        /// <summary>
        /// 系统评分判断 地址类型
        /// </summary>
        public int addr_type { get; set; }

        /// <summary>
        /// 发件人公司名称
        /// </summary>
        public string sender_company { get; set; }
        /// <summary>
        /// 发件人国家
        /// </summary>
        public string sender_country { get; set; }
        /// <summary>
        /// 发件人名称
        /// </summary>
        public string sender_name { get; set; }
        /// <summary>
        /// 收件人名称
        /// </summary>
        public string receiver_name { get; set; }

        /// <summary>
        /// 最新拦截备注:[拦截详情表最后一条]
        /// </summary>
        public string last_intercept_remark { get; set; }
        /// <summary>
        /// 参考编号【G2G美国】
        /// </summary>
        public string reference_number { get; set; }
        /// <summary>
        /// 退件时间
        /// </summary>
        public long? returnon { get; set; }
        ///<summary>
        /// 销售产品组
        ///</summary>
        public string product_group { get; set; }
        ///<summary>
        /// 核销状态
        ///</summary>
        public int? rps_status { get; set; }
        ///<summary>
        /// 核销金额
        ///</summary>
        public decimal rps_amount { get; set; }
    }

    /// <summary>
    /// 收件人
    /// </summary>
    public class OdrDetailsReceiverAddress
    {   /// <summary>  
        /// 订单编号  
        /// </summary>
        public long order_id { get; set; }
        /// <summary>  
        /// 姓  
        /// </summary>
        public string first_name { get; set; }
        /// <summary>  
        /// 名  
        /// </summary>
        public string last_name { get; set; }
        /// <summary>  
        /// 公司名称  
        /// </summary>
        public string company { get; set; }
        /// <summary>  
        /// 国家代码  
        /// </summary>
        public string country_code { get; set; }

        ///// <summary>
        ///// 国家名称
        ///// </summary>
        //public string country_name { get; set; }
        ///// <summary>
        ///// 国家英文名称
        ///// </summary>
        //public string country_enname { get; set; }
        /// <summary>  
        /// 省州  
        /// </summary>
        public string province { get; set; }
        /// <summary>  
        /// 城市  
        /// </summary>
        public string city { get; set; }
        /// <summary>  
        /// 街道1  
        /// </summary>
        public string street1 { get; set; }
        /// <summary>  
        /// 街道2  
        /// </summary>
        public string street2 { get; set; }
        /// <summary>  
        /// 街道3  
        /// </summary>
        public string street3 { get; set; }
        /// <summary>  
        /// 邮编  
        /// </summary>
        public string postal_code { get; set; }
        /// <summary>  
        /// 电话  
        /// </summary>
        public string telephone { get; set; }
        /// <summary>  
        /// 手机  
        /// </summary>
        public string mobile_phone { get; set; }
        /// <summary>  
        /// 传真  
        /// </summary>
        public string fax { get; set; }
        /// <summary>  
        /// 门牌号  
        /// </summary>
        public string house_number { get; set; }
        /// <summary>  
        /// 电子邮箱  
        /// </summary>
        public string email { get; set; }
        /// <summary>  
        /// 是否偏远地址  
        /// </summary>
        public string is_faraway { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string certificate_code { get; set; }

        /// <summary>
        /// 商城账号
        /// </summary>
        public string mall_account { get; set; }

        /// <summary>
        /// 有效期间，由于时间格式不好定义，故用文本类型
        /// </summary>
        public string credentials_period { get; set; }

        /// <summary>
        /// 收件人身份认证类型代码，例如身份证、护照等
        /// </summary>
        public string certificate_type { get; set; }

        /// <summary>
        /// 亚马逊地址Code
        /// </summary>
        public string fba_addrcode { get; set; }

        /// <summary>
        /// 系统判断的fba状态类型 1:fba  2;私人地址
        /// </summary>
        public int addr_type { get; set; }
    }
}
