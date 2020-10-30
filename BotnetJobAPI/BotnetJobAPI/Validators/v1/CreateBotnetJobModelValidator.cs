using System;
using BotnetJobAPI.Models.v1;
using FluentValidation;

namespace BotnetJobAPI.Validators.v1
{
    public class CreateBotnetJobModelValidator : AbstractValidator<CreateBotnetJobModel>
    {
        public CreateBotnetJobModelValidator()
        {
            RuleFor(x => x.BotnetId)
                .NotNull()
                .WithMessage("The command id can not be empty");

            // TODO: Add check for valid command id
            RuleFor(x => x.CommandId)
                .NotNull()
                .WithMessage("The command id can not be empty");
        }
    }
}
