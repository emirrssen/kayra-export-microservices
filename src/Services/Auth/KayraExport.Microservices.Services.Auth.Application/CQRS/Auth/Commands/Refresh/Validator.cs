using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.FluentValidation;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Refresh
{
    public class Validator : ValidatorBase<Command>
    {
        public Validator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Yenileme token'ının belirtilmesi zorunludur");
        }
    }
}
