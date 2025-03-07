using VendorService.Domain.Entities;

namespace VendorService.Infrastructure.Data
{
    public static class VendorDataStore
    {
        public static List<Vendor> Vendors { get; } =
    [
        new Vendor(Guid.NewGuid(), "Vendor One", "vendor1@example.com"),
        new Vendor(Guid.NewGuid(), "Vendor Two", "vendor2@example.com"),
        new Vendor(Guid.NewGuid(), "Vendor Three", "vendor3@example.com")
    ];
    }
}
