using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.FluentValidation;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Login
{
    public class Validator : ValidatorBase<Command>
    {
        public Validator()
        {
            RuleFor(x => x.LoginCredentials)
                .NotEmpty().WithMessage("Lütfen kullanıcı adı veya e-posta adresi giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifrenizi giriniz");
        }
    }
}
