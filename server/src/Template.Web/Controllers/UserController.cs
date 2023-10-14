using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application;
using Template.Application.OrderApp.DTO;

namespace Template.Web.Controllers
{
    [NonController]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly IOrderService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IOrderService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public string UUID()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Order> Get(int id)
        {
            return await _userService.Get(id);
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Order> Find(int id)
        {
            return await _userService.Find(id);
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Order>> GetList(List<int> ids)
        {
            return await _userService.GetList(ids);
        }
    }
}
