using Microsoft.EntityFrameworkCore;
using iForce.Domain;

namespace iForce.App
{
    public class CustomerAccess : ICustomerAccess
    {
        private readonly IDataContext _context;

        public CustomerAccess(IDataContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Customer> Get(CustomerFilter? filter)
        {
            filter ??= new CustomerFilter();
            var query = from c in _context.Customers select c;
            if (filter.MinDateOfBirth.HasValue)
            {
                query = from q in query where q.DateOfBirth >= filter.MinDateOfBirth.Value select q;
            }
            if (filter.MaxDateOfBirth.HasValue)
            {
                query = from q in query where q.DateOfBirth <= filter.MaxDateOfBirth.Value select q;
            }
            if (filter.IncludeVehicles)
            {
                query = query.Include(c => c.Vehicles);
            }
            return query.ToList();
        }

        public int Create(CustomerBase customer)
        {
            var row = new Customer();
            row.Update(customer);
            _context.Customers.Add(row);
            _context.SaveChanges();
            return row.CustomerId;
        }

        public bool Change(int id, CustomerBase customer)
        {
            var old = (from c in _context.Customers where c.CustomerId == id select c)
                .AsTracking()
                .FirstOrDefault();
            if (old != null)
            {
                old.Update(customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var old = (from c in _context.Customers where c.CustomerId == id select c)
                .AsTracking()
                .FirstOrDefault();
            if (old != null)
            {
                _context.Customers.Remove(old);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
