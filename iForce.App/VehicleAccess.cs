using iForce.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iForce.App
{
    public class VehicleAccess : IVehicleAccess
    {
        private readonly IDataContext _context;

        public VehicleAccess(IDataContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Vehicle> Get(VehicleFilter? filter)
        {
            filter ??= new VehicleFilter();
            var query = from v in _context.Vehicles select v;
            if (filter.MaxRegistrationDate.HasValue)
            {
                query = from q in query where q.RegistrationDate <= filter.MaxRegistrationDate.Value select q;
            }
            if (filter.MinEngineSizeCc.HasValue)
            {
                query = from q in query where q.EngineSizeCc >= filter.MinEngineSizeCc.Value select q;
            }
            return query.ToList();
        }

        public int Create(VehicleBase vehicle)
        {
            var row = new Vehicle();
            row.Update(vehicle);
            _context.Vehicles.Add(row);
            _context.SaveChanges();
            return row.VehicleId;
        }

        public bool Change(int id, VehicleBase vehicle)
        {
            var old = (from v in _context.Vehicles where v.VehicleId == id select v)
                .AsTracking()
                .FirstOrDefault();
            if (old != null)
            {
                old.Update(vehicle);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var old = (from v in _context.Vehicles where v.VehicleId == id select v)
                .AsTracking()
                .FirstOrDefault();
            if (old != null)
            {
                _context.Vehicles.Remove(old);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
