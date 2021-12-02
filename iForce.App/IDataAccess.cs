using iForce.Domain;

namespace iForce.App
{
    public interface IDataAccess<T, TBase, TFilter> where T : TBase
    {
        IReadOnlyList<T> Get(TFilter? filter);
        int Create(TBase item);
        bool Change(int id, TBase item);
        bool Delete(int id);
    }

    public interface ICustomerAccess : IDataAccess<Customer, CustomerBase, CustomerFilter> { }
    public interface IVehicleAccess : IDataAccess<Vehicle, VehicleBase, VehicleFilter> { }
}