using FluentValidation;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(user => user.Phone).NotEmpty().Matches("^[0-9]{10,15}$").WithMessage("Phone must be a valid number.");
            RuleFor(user => user.Role).NotEmpty().Must(role => new[] { "Admin", "Supervisor", "Staff" }.Contains(role)).WithMessage("Invalid role.");
        }
    }
}
