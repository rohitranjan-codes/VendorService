using MediatR;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Features.Commands
{
    public class DeleteVendorCommandHandler(IVendorRepository vendorRepository) : IRequestHandler<DeleteVendorCommand, Unit>
    {
        public readonly IVendorRepository _vendorRepository = vendorRepository;
        public async Task<Unit> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetVendorByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Vendor with ID {request.Id} not found.");
            await _vendorRepository.DeleteVendorAsync(request.Id);

            return Unit.Value;
        }
    }
}
