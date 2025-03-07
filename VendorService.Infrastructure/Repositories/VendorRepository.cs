using VendorService.Domain.Entities;
using VendorService.Domain.Repositories;
using VendorService.Infrastructure.Data;

namespace VendorService.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {

            return await Task.FromResult(VendorDataStore.Vendors);
        }

        public async Task<Vendor?> GetVendorByIdAsync(Guid id)
        {
            return await Task.FromResult(VendorDataStore.Vendors.FirstOrDefault(v => v.Id == id));
        }

        public async Task<Guid> AddVendorAsync(string name, string email)
        {
            var vendor = new Vendor(Guid.NewGuid(), name, email);
            VendorDataStore.Vendors.Add(vendor);
            return await Task.FromResult(vendor.Id);
        }

        public async Task<Guid> UpdateVendorAsync(Guid id, string name, string email)
        {
            var vendor = VendorDataStore.Vendors.FirstOrDefault(v => v.Id == id);
            if (vendor != null)
            {
                vendor.Update(name, email);
                return await Task.FromResult(vendor.Id);
            }
            return await Task.FromResult(Guid.Empty);
        }

        public async Task DeleteVendorAsync(Guid id)
        {
            var vendor = VendorDataStore.Vendors.FirstOrDefault(v => v.Id == id);
            if (vendor != null)
            {
                VendorDataStore.Vendors.Remove(vendor);
            }
            await Task.CompletedTask;
        }
    }
}
