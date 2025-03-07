using MediatR;
using VendorService.Application.DTOs;

namespace VendorService.Application.Features.Queries
{
    public class GetAllVendorsQuery : IRequest<IEnumerable<VendorDto>>
    {
    }
}
