using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private AppDbContext _context;
        private OrderService _orderService;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Setup DbContext with TestConnection
            _context = new AppDbContext("TestConnection");
            _context.Database.EnsureCreated(); // Ensure the database is created
            _orderService = new OrderService(_context);

        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            _context.Database.EnsureDeleted(); // Delete the database after tests
        }

        [SetUp]
        public void Setup()
        {
            _context.Orders.RemoveRange(_context.Orders);
            _context.OrderItems.RemoveRange(_context.OrderItems);
            _context.SaveChanges();
        }

        [Test]
        public void OrderService_AddOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerName = "John Doe"
            };

            // Act
            _orderService.AddOrder(order);

            // Assert
            var addedOrder = _context.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
            Assert.That(addedOrder, Is.Not.Null);
            Assert.That(addedOrder.OrderDate.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void OrderService_GetOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerName = "Jane Doe"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Act
            var foundOrder = _orderService.GetOrder(order.OrderId);

            // Assert
            Assert.That(foundOrder, Is.Not.Null);
            Assert.That(foundOrder.OrderId, Is.EqualTo(order.OrderId));
        }

        [Test]
        public void OrderService_UpdateOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now.AddDays(-1),
                CustomerName = "Update Test"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Act
            order.CustomerName = "Updated Name";
            _orderService.UpdateOrder(order);

            // Assert
            var updatedOrder = _context.Orders.Find(order.OrderId);
            Assert.That(updatedOrder.CustomerName, Is.EqualTo("Updated Name"));
        }

        [Test]
        public void OrderService_DeleteOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerName = "Delete Test"
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Act
            _orderService.DeleteOrder(order.OrderId);

            // Assert
            var deletedOrder = _context.Orders.Find(order.OrderId);
            Assert.That(deletedOrder, Is.Null);
        }

        [Test]
        public void OrderService_GetAllOrders()
        {
            // Arrange
            var order1 = new Order { OrderDate = DateTime.Now, CustomerName = "All Test 1" };
            var order2 = new Order { OrderDate = DateTime.Now, CustomerName = "All Test 2" };
            _context.Orders.AddRange(order1, order2);
            _context.SaveChanges();

            // Act
            var orders = _orderService.GetAllOrders();

            // Assert
            Assert.That(orders.Count, Is.EqualTo(2));
            Assert.That(orders.Select(o => o.CustomerName), Contains.Item("All Test 1"));
            Assert.That(orders.Select(o => o.CustomerName), Contains.Item("All Test 2"));
        }
    }
}
