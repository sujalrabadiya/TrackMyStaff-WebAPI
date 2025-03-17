using FluentValidation;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Validators
{
    public class NotificationValidator : AbstractValidator<NotificationModel>
    {
        public NotificationValidator()
        {
            RuleFor(notification => notification.UserId).GreaterThan(0).WithMessage("User ID is required.");
            RuleFor(notification => notification.Title).NotEmpty().WithMessage("Title is required.");
        }
    }
}
