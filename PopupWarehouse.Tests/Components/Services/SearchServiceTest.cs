using NUnit.Framework;
using Data;
using Services;
using Models;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class SearchServiceTests
    {
        private AppDbContext _context;
        private ProductService _productService;
        private OrderService _orderService;
        private ShipmentService _shipmentService;
        private SearchService _searchService;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Setup DbContext with TestConnection
            _context = new AppDbContext("TestConnection");
            _context.Database.EnsureCreated();

            // Initialize services with the DbContext
            _productService = new ProductService(_context);
            _orderService = new OrderService(_context);
            _shipmentService = new ShipmentService(_context);
            _searchService = new SearchService(_productService, _orderService, _shipmentService);

            // Ensure a clean state before tests
            _context.Products.RemoveRange(_context.Products);
            _context.Orders.RemoveRange(_context.Orders);
            _context.Shipments.RemoveRange(_context.Shipments);
            _context.SaveChanges();

            // Insert test data for Products, Orders, and Shipments
            var product = new Product { Name = "Test Product", Quantity = 10, Description = "Test Description" };
            _productService.AddProduct(product);

            var order = new Order { OrderDate = DateTime.Now, CustomerName = "Test Customer" };
            _orderService.AddOrder(order);

            var shipment = new Shipment { Destination = "Test Destination", ShipmentDate = DateTime.Now.AddDays(1) };
            _shipmentService.AddShipment(shipment);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Clean up the database after tests
            //_context.Database.EnsureDeleted();
            //_context.Dispose();
        }

        [Test]
        public void SearchService_ProductSearch()
        {
            // Arrange
            var addedProduct = _context.Products.FirstOrDefault(p => p.Name == "Test Product");
            
            // Act
            var result = _searchService.Search("I" + addedProduct.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Product>());

            var productResult = result as Product;
            Assert.That(productResult.Id, Is.EqualTo(addedProduct.Id));
            Assert.That(productResult.Name, Is.EqualTo("Test Product"));
        }

        [Test]
        public void SearchService_OrderSearch()
        {
            // Arrange
            var addedOrder = _context.Orders.FirstOrDefault(o => o.CustomerName == "Test Customer");

            // Act
            var result = _searchService.Search("O" + addedOrder.OrderId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Order>());

            var orderResult = result as Order;
            Assert.That(orderResult.OrderId, Is.EqualTo(addedOrder.OrderId));
            Assert.That(orderResult.CustomerName, Is.EqualTo("Test Customer"));
        }

        [Test]
        public void SearchService_ShipmentSearch()
        {
            // Arrange
            var addedShipment = _context.Shipments.FirstOrDefault(s => s.Destination == "Test Destination");

            // Act
            var result = _searchService.Search("S" + addedShipment.ShipmentId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Shipment>());

            var shipmentResult = result as Shipment;
            Assert.That(shipmentResult.ShipmentId, Is.EqualTo(addedShipment.ShipmentId));
            Assert.That(shipmentResult.Destination, Is.EqualTo("Test Destination"));
        }
    }
}
