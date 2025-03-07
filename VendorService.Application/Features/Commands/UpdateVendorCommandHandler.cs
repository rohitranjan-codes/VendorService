using MediatR;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Features.Commands
{
    public class UpdateVendorCommandHandler(IVendorRepository vendorRepository) : IRequestHandler<UpdateVendorCommand, Guid>
    {
        public readonly IVendorRepository _vendorRepository = vendorRepository;
        public async Task<Guid> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetVendorByIdAsync(request.Id);
            return vendor == null
                ? throw new KeyNotFoundException($"Vendor with ID {request.Id} not found.")
                : await _vendorRepository.UpdateVendorAsync(request.Id, request.RequestVendorDto.Name, request.RequestVendorDto.Email);
        }
    }
}
