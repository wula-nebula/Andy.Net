using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.OrderApp.DTO;

namespace Template.Application
{
    public interface IOrderService
    {
        Task<Order> Find(long id);
        Task<Order> Get(long id);
        Task<List<Order>> GetList(List<int> id);
    }
}
