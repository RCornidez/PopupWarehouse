using NUnit.Framework;
using Data;
using Models;
using Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class SupplierServiceTests
    {
        private AppDbContext _context;
        private SupplierService _supplierService;
        
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Setup DbContext with TestConnection
            _context = new AppDbContext("TestConnection");
            _context.Database.EnsureCreated(); // Ensure the database is created
            _supplierService = new SupplierService(_context);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            //_context.Database.EnsureDeleted(); // Delete the database after tests
        }

        [SetUp]
        public void Setup()
        {
            _context.Suppliers.RemoveRange(_context.Suppliers);
            _context.SaveChanges();
        }

        [Test]
        public void SupplierService_AddSupplierToDatabase()
        {
            // Arrange
            var supplier = new Supplier { Name = "Test Supplier", Address = "123 Test St" };

            // Act
            _supplierService.AddSupplier(supplier);

            // Assert
            Assert.That(_context.Suppliers.Count(), Is.EqualTo(1));
            var dbSupplier = _context.Suppliers.First();

            Assert.That(dbSupplier.Name, Is.EqualTo("Test Supplier"));
            Assert.That(dbSupplier.Address, Is.EqualTo("123 Test St"));
        }

        [Test]
        public void SupplierService_GetSupplier()
        {
            // Arrange
            var supplier = new Supplier { Name = "Test Supplier", Address = "123 Test St" };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            // Act
            var result = _supplierService.GetSupplier(supplier.SupplierId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SupplierId, Is.EqualTo(supplier.SupplierId));
            Assert.That(result.Name, Is.EqualTo("Test Supplier"));
            Assert.That(result.Address, Is.EqualTo("123 Test St"));
        }

        [Test]
        public void SupplierService_UpdateSupplier()
        {
            // Arrange
            var supplier = new Supplier { Name = "Old Name", Address = "Old Address" };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            supplier.Name = "New Name";
            supplier.Address = "New Address";

            // Act
            _supplierService.UpdateSupplier(supplier);
            var result = _context.Suppliers.Find(supplier.SupplierId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("New Name"));
            Assert.That(result.Address, Is.EqualTo("New Address"));
        }

        [Test]
        public void SupplierService_DeleteSupplier()
        {
            // Arrange
            var supplier = new Supplier { Name = "Test Supplier", Address = "123 Test St" };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            // Act
            _supplierService.DeleteSupplier(supplier.SupplierId);

            // Assert
            var result = _context.Suppliers.Find(supplier.SupplierId);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void SupplierService_GetAllSuppliers()
        {
            // Arrange
            var supplier1 = new Supplier { Name = "Supplier 1", Address = "Address 1" };
            var supplier2 = new Supplier { Name = "Supplier 2", Address = "Address 2" };
            _context.Suppliers.AddRange(supplier1, supplier2);
            _context.SaveChanges();

            // Act
            var result = _supplierService.GetAllSuppliers();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Contains.Item(supplier1));
            Assert.That(result, Contains.Item(supplier2));
        }
    }
}
