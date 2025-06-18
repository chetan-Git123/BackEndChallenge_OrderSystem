using Cricut.Orders.Domain.Models;

namespace Cricut.Orders.Domain
{
    public interface IOrderDomain
    {
        Task<Order> CreateNewOrderAsync(Order order);
    }

    public class OrderDomain : IOrderDomain
    {
        private readonly IOrderStore _orderStore;

        public OrderDomain(IOrderStore orderStore)
        {
            _orderStore = orderStore;
        }
        
        /* Old implementation
        public async Task<Order> CreateNewOrderAsync(Order order)
        {
            var updatedOrder = await _orderStore.SaveOrderAsync(order);
            return updatedOrder;

        }
        */

        //New implemebtation
        public async Task<Order> CreateNewOrderAsync(Order order)
        {
                // Calculate subtotal
               decimal subtotal = order.OrderItems.Sum(item => item.Product.Price * item.Quantity);

               // Apply 10% discount if subtotal is $25 or more
               if (subtotal >= 25.0m)
               {
                 order.Total = subtotal * 0.9m; // Apply 10% discount
               }
               else
               {
                order.Total = subtotal;
               }

             order.Total = Math.Round(order.Total, 2); // Round to 2 decimal places

             var updatedOrder = await _orderStore.SaveOrderAsync(order);
             return updatedOrder;
        }
        
        
    }
}
