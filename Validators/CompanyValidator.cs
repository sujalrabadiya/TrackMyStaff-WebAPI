using FluentValidation;
using TrackMyStaffWebApplication.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TrackMyStaffWebApplication.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyModel>
    {
        public CompanyValidator()
        {
            RuleFor(company => company.name).NotEmpty().WithMessage("Company name is required.");
            RuleFor(company => company.email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
            RuleFor(company => company.phone).NotEmpty().Matches("^[0-9]{10,15}$").WithMessage("Phone must be a valid number.");
        }
    }
}
