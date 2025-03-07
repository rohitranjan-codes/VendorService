using VendorService.Domain.Entities;

namespace VendorService.Domain.Repositories
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor?> GetVendorByIdAsync(Guid id);
        Task<Guid> AddVendorAsync(string name, string email);
        Task<Guid> UpdateVendorAsync(Guid id, string name, string email);
        Task DeleteVendorAsync(Guid id);
    }
}
