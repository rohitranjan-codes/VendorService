using AutoMapper;
using MediatR;
using VendorService.Application.DTOs;
using VendorService.Domain.Entities;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Features.Queries
{
    public class GetVendorByIdQueryHandler(IVendorRepository vendorRepository, IMapper mapper) : IRequestHandler<GetVendorByIdQuery, VendorDto?>
    {
        private readonly IVendorRepository _vendorRepository = vendorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<VendorDto?> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetVendorByIdAsync(request.Id);
            if (vendor == null) return null;

            return _mapper.Map<VendorDto>(vendor);
        }
    }
}
