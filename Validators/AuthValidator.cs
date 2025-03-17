using FluentValidation;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Validators
{
    public class RegisterationValidator : AbstractValidator<RegisterationModel>
    {
        public RegisterationValidator()
        {
            RuleFor(x => x.CName)
                .NotEmpty().WithMessage("Company Name is required.")
                .MaximumLength(100).WithMessage("Company Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Company Email is required.")
                .EmailAddress().WithMessage("A valid email is required.")
                .MaximumLength(100).WithMessage("Company Email cannot exceed 100 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Company Phone is required.")
                .Matches("^[0-9]{10,15}$").WithMessage("Company Phone must be a valid number between 10 and 15 digits.");

            RuleFor(x => x.UName)
                .NotEmpty().WithMessage("User Name is required.")
                .MaximumLength(100).WithMessage("User Name cannot exceed 100 characters.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("User Password is required.")
               .MinimumLength(6).WithMessage("User Password must be at least 6 characters long.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("User Role is required.")
                .Must(role => new[] { "Admin", "Supervisor", "Staff" }.Contains(role))
                .WithMessage("Invalid role.");
        }
    }

    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator ()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required for login.")
                .Matches("^[0-9]{10,15}$").WithMessage("Phone must be a valid number between 10 and 15 digits.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required for login.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
