using AutoMapper;
using MediatR;
using VendorService.Application.DTOs;
using VendorService.Domain.Repositories;

namespace VendorService.Application.Features.Queries
{
    public class GetAllVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper) : IRequestHandler<GetAllVendorsQuery, IEnumerable<VendorDto>>
    {
        private readonly IVendorRepository _vendorRepository = vendorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<VendorDto>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            var vendors = await _vendorRepository.GetAllVendorsAsync();
            return _mapper.Map<IEnumerable<VendorDto>>(vendors);
            
        }
    }
}
