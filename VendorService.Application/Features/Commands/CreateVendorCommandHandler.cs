using MediatR;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Features.Commands
{
    public class CreateVendorCommandHandler(IVendorRepository vendorRepository) : IRequestHandler<CreateVendorCommand, Guid>
    {
        public readonly IVendorRepository _vendorRepository = vendorRepository;
        public async Task<Guid> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var Id = await _vendorRepository.AddVendorAsync(request.RequestVendorDto.Name, request.RequestVendorDto.Email);
            return Id;
        }
    }
}
