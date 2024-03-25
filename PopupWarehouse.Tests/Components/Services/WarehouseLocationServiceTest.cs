using NUnit.Framework;
using Data;
using Models;
using Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class WarehouseLocationServiceTests
    {
        private AppDbContext _context;
        private WarehouseLocationService _locationService;
         
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Setup DbContext with TestConnection
            _context = new AppDbContext("TestConnection");
            _context.Database.EnsureCreated(); // Ensure the database is created
            _locationService = new WarehouseLocationService(_context);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            //_context.Database.EnsureDeleted(); // Delete the database after tests
        }

        [SetUp]
        public void Setup()
        {
            _context.WarehouseLocations.RemoveRange(_context.WarehouseLocations);
            _context.SaveChanges();
        }

        [Test]
        public void WarehouseLocationService_AddLocationToDatabase()
        {
            // Arrange
            var location = new WarehouseLocation { LocationName = "Warehouse A", Description = "Location A Description" };

            // Act
            _locationService.AddLocation(location);

            // Assert
            Assert.That(_context.WarehouseLocations, Has.Exactly(1).Items);
            var dbLocation = _context.WarehouseLocations.First();
            Assert.Multiple(() =>
            {
                Assert.That(dbLocation.LocationName, Is.EqualTo("Warehouse A"), "Location name should match");
                Assert.That(dbLocation.Description, Is.EqualTo("Location A Description"), "Description should match");
            });
        }

        [Test]
        public void WarehouseLocationService_GetLocation()
        {
            // Arrange
            var location = new WarehouseLocation { LocationName = "Warehouse B", Description = "Location B Description" };
            _context.WarehouseLocations.Add(location);
            _context.SaveChanges();

            // Act
            var result = _locationService.GetLocation(location.LocationId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null, "Result should not be null");
                Assert.That(result.LocationId, Is.EqualTo(location.LocationId), "Location ID should match");
                Assert.That(result.LocationName, Is.EqualTo("Warehouse B"), "Location name should match");
                Assert.That(result.Description, Is.EqualTo("Location B Description"), "Description should match");
            });
        }

        [Test]
        public void WarehouseLocationService_UpdateLocation()
        {
            // Arrange
            var location = new WarehouseLocation { LocationName = "Warehouse C", Description = "Location C Description" };
            _context.WarehouseLocations.Add(location);
            _context.SaveChanges();

            // Update Info
            location.LocationName = "Updated Warehouse C";
            location.Description = "Updated Description C";

            // Act
            _locationService.UpdateLocation(location);

            // Assert
            var updatedLocation = _context.WarehouseLocations.Find(location.LocationId);
            Assert.Multiple(() =>
            {
                Assert.That(updatedLocation, Is.Not.Null, "Updated location should not be null");
                Assert.That(updatedLocation.LocationName, Is.EqualTo("Updated Warehouse C"), "Location name should be updated");
                Assert.That(updatedLocation.Description, Is.EqualTo("Updated Description C"), "Description should be updated");
            });
        }

        [Test]
        public void WarehouseLocationService_DeleteLocation()
        {
            // Arrange
            var location = new WarehouseLocation { LocationName = "Warehouse D", Description = "Location D Description" };
            _context.WarehouseLocations.Add(location);
            _context.SaveChanges();

            // Act
            _locationService.DeleteLocation(location.LocationId);

            // Assert
            var deletedLocation = _context.WarehouseLocations.Find(location.LocationId);
            Assert.That(deletedLocation, Is.Null, "Deleted location should not exist in the database");
        }

        [Test]
        public void WarehouseLocationService_GetAllLocations()
        {
            // Arrange
            var location1 = new WarehouseLocation { LocationName = "Warehouse E", Description = "Location E Description" };
            var location2 = new WarehouseLocation { LocationName = "Warehouse F", Description = "Location F Description" };
            _context.WarehouseLocations.AddRange(location1, location2);
            _context.SaveChanges();

            // Act
            var allLocations = _locationService.GetAllLocations();

            // Assert
            Assert.That(allLocations, Has.Exactly(2).Items, "Should retrieve all locations");
            Assert.That(allLocations, Contains.Item(location1).And.Contains(location2), "Should contain both locations");
        }
    }
}
