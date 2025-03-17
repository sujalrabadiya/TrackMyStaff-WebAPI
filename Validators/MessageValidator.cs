using FluentValidation;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Validators
{
    public class MessageValidator : AbstractValidator<MessageModel>
    {
        public MessageValidator()
        {
            RuleFor(message => message.SenderId).GreaterThan(0).WithMessage("Sender ID is required.");
        }
    }
}
