using iForce.Domain;
using Microsoft.EntityFrameworkCore;

namespace iForce.App
{
    public interface IDataContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Vehicle> Vehicles { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}