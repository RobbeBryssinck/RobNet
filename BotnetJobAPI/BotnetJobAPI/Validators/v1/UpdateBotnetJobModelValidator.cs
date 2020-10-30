using System;
using BotnetJobAPI.Models.v1;
using FluentValidation;

namespace BotnetJobAPI.Validators.v1
{
    public class UpdateBotnetJobModelValidator : AbstractValidator<UpdateBotnetJobModel>
    {
        public UpdateBotnetJobModelValidator()
        {
            RuleFor(x => x.BotnetId)
                .NotNull()
                .WithMessage("The command id can not be empty");

            RuleFor(x => x.CommandId)
                .NotNull()
                .WithMessage("The command id can not be empty");
        }
    }
}
