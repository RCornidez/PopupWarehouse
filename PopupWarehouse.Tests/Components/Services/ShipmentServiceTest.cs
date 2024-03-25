using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Services;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class ShipmentServiceTests
    {
        private AppDbContext _context;
        private ShipmentService _shipmentService;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Setup database connection
            _context = new AppDbContext("TestConnection");
            _context.Database.EnsureCreated(); // Ensure the database is created
            _shipmentService = new ShipmentService(_context);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
           // _context.Database.EnsureDeleted(); // Clean up by deleting the database after tests
        }

        [SetUp]
        public void Setup()
        {
            // Ensure a clean state before each test
            _context.Shipments.RemoveRange(_context.Shipments);
            _context.SaveChanges();
        }

        [Test]
        public void ShipmentService_AddShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                Destination = "New York",
                ShipmentDate = DateTime.UtcNow
            };

            // Act
            _shipmentService.AddShipment(shipment);

            // Assert
            var addedShipment = _context.Shipments.FirstOrDefault(s => s.Destination == "New York");
            Assert.That(addedShipment, Is.Not.Null);
            Assert.That(addedShipment.Destination, Is.EqualTo("New York"));
            Assert.That(addedShipment.ShipmentDate.Date, Is.EqualTo(DateTime.UtcNow.Date));
        }

        [Test]
        public void ShipmentService_GetShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                Destination = "Los Angeles",
                ShipmentDate = DateTime.UtcNow.AddDays(-1)
            };
            _context.Shipments.Add(shipment);
            _context.SaveChanges();

            // Act
            var foundShipment = _shipmentService.GetShipment(shipment.ShipmentId);

            // Assert
            Assert.That(foundShipment, Is.Not.Null);
            Assert.That(foundShipment.ShipmentId, Is.EqualTo(shipment.ShipmentId));
        }

        [Test]
        public void ShipmentService_UpdateShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                Destination = "Chicago",
                ShipmentDate = DateTime.UtcNow.AddDays(-2)
            };
            _context.Shipments.Add(shipment);
            _context.SaveChanges();

            // Act
            shipment.Destination = "Updated Destination";
            _shipmentService.UpdateShipment(shipment);

            // Assert
            var updatedShipment = _context.Shipments.Find(shipment.ShipmentId);
            Assert.That(updatedShipment.Destination, Is.EqualTo("Updated Destination"));
        }

        [Test]
        public void ShipmentService_DeleteShipment()
        {
            // Arrange
            var shipment = new Shipment
            {
                Destination = "Seattle",
                ShipmentDate = DateTime.UtcNow.AddDays(-3)
            };
            _context.Shipments.Add(shipment);
            _context.SaveChanges();

            // Act
            _shipmentService.DeleteShipment(shipment.ShipmentId);

            // Assert
            var deletedShipment = _context.Shipments.Find(shipment.ShipmentId);
            Assert.That(deletedShipment, Is.Null);
        }

        [Test]
        public void ShipmentService_GetAllShipments()
        {
            // Arrange
            var shipment1 = new Shipment { Destination = "Boston", ShipmentDate = DateTime.UtcNow.AddDays(-4) };
            var shipment2 = new Shipment { Destination = "San Francisco", ShipmentDate = DateTime.UtcNow.AddDays(-5) };
            _context.Shipments.AddRange(shipment1, shipment2);
            _context.SaveChanges();

            // Act
            var allShipments = _shipmentService.GetAllShipments();

            // Assert
            Assert.That(allShipments.Count, Is.EqualTo(2));
            Assert.That(allShipments, Has.One.EqualTo(shipment1).And.One.EqualTo(shipment2));
        }
    }
}
