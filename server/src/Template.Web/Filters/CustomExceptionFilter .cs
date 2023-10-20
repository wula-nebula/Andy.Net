using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Template.Web.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // 检查是否抛出异常
            if (context.Exception != null)
            {
                // 在此处可以对异常进行处理，例如记录日志或返回错误响应

                // 将异常信息发送到日志记录器
                context.HttpContext.RequestServices.GetService<ILogger<CustomExceptionFilter>>().LogError(context.Exception, context.Exception.Message);

                // 如果需要，可以返回自定义的错误响应
                context.Result = new ObjectResult(new
                {
                    code = context.Exception.HResult,
                    masage = context.Exception.Message
                });
            }
        }
    }
}
