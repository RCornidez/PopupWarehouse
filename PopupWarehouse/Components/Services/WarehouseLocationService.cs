using Data;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class WarehouseLocationService
    {
        private readonly AppDbContext _context;

        public WarehouseLocationService(AppDbContext context)
        {
            _context = context;
        }

        // Create
        public void AddLocation(WarehouseLocation location)
        {
            _context.WarehouseLocations.Add(location);
            _context.SaveChanges();
        }

        // Read
        public WarehouseLocation GetLocation(int id)
        {
            return _context.WarehouseLocations.Find(id);
        }

        // Update
        public void UpdateLocation(WarehouseLocation location)
        {
            _context.WarehouseLocations.Update(location);
            _context.SaveChanges();
        }

        // Delete
        public void DeleteLocation(int id)
        {
            var location = _context.WarehouseLocations.Find(id);
            if (location != null)
            {
                _context.WarehouseLocations.Remove(location);
                _context.SaveChanges();
            }
        }

        // List All
        public List<WarehouseLocation> GetAllLocations()
        {
            return _context.WarehouseLocations.ToList();
        }
    }
}
