using FluentValidation;
using VendorService.Application.DTOs;

namespace VendorService.Application.Validators
{
    public class RequestVendorDtoValidator : AbstractValidator<RequestVendorDto>
    {
        public RequestVendorDtoValidator()
        {
            RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Vendor name is required.")
            .MaximumLength(100).WithMessage("Vendor name must be less than 100 characters.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
