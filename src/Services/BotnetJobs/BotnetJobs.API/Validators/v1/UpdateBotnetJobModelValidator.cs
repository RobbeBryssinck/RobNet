using BotnetJobs.API.Models.v1;
using FluentValidation;

namespace BotnetJobs.API.Validators.v1
{
    public class UpdateBotnetJobModelValidator : AbstractValidator<UpdateBotnetJobModel>
    {
        public UpdateBotnetJobModelValidator()
        {
            RuleFor(x => x.BotnetId)
                .NotNull()
                .WithMessage("The botnet id can not be empty");

            RuleFor(x => x.CommandId)
                .NotNull()
                .WithMessage("The command id can not be empty");

            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage("The status can not be empty");
        }
    }
}
