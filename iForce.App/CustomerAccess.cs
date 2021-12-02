using Microsoft.EntityFrameworkCore;
using iForce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var customers = query.ToList();
            if (filter.IncludeVehicles)
            {
                foreach (var customer in customers)
                {
                    customer.Vehicles = (from v in _context.Vehicles where v.CustomerId == customer.CustomerId select v).ToList();
                }
            }
            return customers;
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
