using FluentAssertions;
using Moq;
using VendorService.Application.DTOs;
using VendorService.Application.Features.Commands;
using VendorService.Domain.Repositories;

namespace VendorService.Tests.Handlers
{
    public class CreateVendorCommandHandlerTests
    {
        private readonly Mock<IVendorRepository> _vendorRepositoryMock;
        private readonly CreateVendorCommandHandler _handler;

        public CreateVendorCommandHandlerTests()
        {
            _vendorRepositoryMock = new Mock<IVendorRepository>();
            _handler = new CreateVendorCommandHandler(_vendorRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_CreateVendor_When_ValidDataProvided()
        {
            var requestDto = new RequestVendorDto { Name = "Test Vendor", Email = "test@example.com" };
            var command = new CreateVendorCommand(requestDto);
            var generatedVendorId = Guid.NewGuid();


            _vendorRepositoryMock.Setup(repo => repo.AddVendorAsync(It.IsAny<string>(), It.IsAny<string>()))
                                 .ReturnsAsync(generatedVendorId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(generatedVendorId);

            _vendorRepositoryMock.Verify(repo => repo.AddVendorAsync(requestDto.Name, requestDto.Email), Times.Once);

        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_RepositoryFails()
        {

            var requestDto = new RequestVendorDto { Name = "Test Vendor", Email = "test@example.com" };
            var command = new CreateVendorCommand(requestDto);

            _vendorRepositoryMock.Setup(repo => repo.AddVendorAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));


            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
        }
    }
}

