using BotnetJobs.API.Models.v1;
using FluentValidation;

namespace BotnetJobs.API.Validators.v1
{
    public class CreateBotnetJobModelValidator : AbstractValidator<CreateBotnetJobModel>
    {
        public CreateBotnetJobModelValidator()
        {
            RuleFor(x => x.BotnetId)
                .NotNull()
                .WithMessage("The botnet id can not be empty");

            // TODO: Add check for valid command id
            RuleFor(x => x.CommandId)
                .NotNull()
                .WithMessage("The command id can not be empty");

            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage("The status can not be empty");

            RuleFor(x => x.Command)
                .NotNull()
                .WithMessage("The command can not be empty");
        }
    }
}
