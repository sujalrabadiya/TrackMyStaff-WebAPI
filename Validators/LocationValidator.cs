using FluentValidation;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Validators
{
    public class LocationValidator : AbstractValidator<LocationModel>
    {
        public LocationValidator()
        {
            RuleFor(location => location.UserId).GreaterThan(0).WithMessage("User ID is required.");
            RuleFor(location => location.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
            RuleFor(location => location.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
        }
    }
}
