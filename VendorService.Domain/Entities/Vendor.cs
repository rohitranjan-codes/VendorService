namespace VendorService.Domain.Entities
{
    public class Vendor
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public Vendor(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
