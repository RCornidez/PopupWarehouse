using Microsoft.EntityFrameworkCore;
using Models;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // Add an order
        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        // Get an order by ID
        public Order GetOrder(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        // Update an order
        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        // Delete an order
        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        // List all orders
        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ToList();
        }
    }
}
