using MediatR;

namespace VendorService.Application.Features.Commands
{
    public class DeleteVendorCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; } = id;

    }
}
