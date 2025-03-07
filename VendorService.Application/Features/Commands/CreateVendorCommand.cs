using MediatR;
using VendorService.Application.DTOs;

namespace VendorService.Application.Features.Commands
{
    public class CreateVendorCommand : IRequest<Guid>
    {
        public RequestVendorDto RequestVendorDto { get; set; }


        public CreateVendorCommand() { }

        public CreateVendorCommand(RequestVendorDto requestVendorDto)
        {
            RequestVendorDto = requestVendorDto;
        }
    }
}
