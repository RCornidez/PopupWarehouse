using Models;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ShipmentService
    {
        private readonly AppDbContext _context;

        public ShipmentService(AppDbContext context)
        {
            _context = context;
        }

        public void AddShipment(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            _context.SaveChanges();
        }

        public Shipment GetShipment(int shipmentId)
        {
            return _context.Shipments.Find(shipmentId);
        }

        public void UpdateShipment(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            _context.SaveChanges();
        }

        public void DeleteShipment(int shipmentId)
        {
            var shipment = _context.Shipments.Find(shipmentId);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                _context.SaveChanges();
            }
        }

        public List<Shipment> GetAllShipments()
        {
            return _context.Shipments.ToList();
        }
    }
}
