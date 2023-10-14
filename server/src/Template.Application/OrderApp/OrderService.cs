using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application.OrderApp.DTO;
using Template.Dapper.Repositories;

namespace Template.Application
{
    public class OrderService : IOrderService
    {
        private readonly IDapperRepository<Order> _orderRepository;

        private readonly IDapperRepository _repository;
        public OrderService(IDapperRepository<Order> orderRepository, IDapperRepository repository)
        {
            _orderRepository = orderRepository;
            _repository = repository;
        }
        public async Task<Order> Find(long id)
        {
            return await _orderRepository.GetAsync(id);
        }

        public async Task<Order> Get(long id)
        {
            return await _orderRepository.FindAsync<Order>($"select * from csd_orders where order_id={id}", new { order_id = id });
        }

        public async Task<List<Order>> GetList(List<int> ids)
        {
            var list = await _repository.QueryAsync<Order>("select * from csd_orders where order_id in @ids", new { ids });
            return list.ToList();
        }
    }
}
