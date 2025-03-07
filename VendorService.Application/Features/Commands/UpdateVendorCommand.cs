using MediatR;
using VendorService.Application.DTOs;

namespace VendorService.Application.Features.Commands
{
    public class UpdateVendorCommand : IRequest<Guid>
    {
        public RequestVendorDto RequestVendorDto { get; set; }
        public Guid Id { get; set; }


        public UpdateVendorCommand() { }

        public UpdateVendorCommand(RequestVendorDto requestVendorDto, Guid id)
        {
            RequestVendorDto = requestVendorDto;
            Id = id;
        }
    }
}
