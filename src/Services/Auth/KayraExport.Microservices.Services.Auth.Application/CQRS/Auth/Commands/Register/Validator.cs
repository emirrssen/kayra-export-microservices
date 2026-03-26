using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.FluentValidation;

namespace KayraExport.Microservices.Services.Auth.Application.CQRS.Auth.Commands.Register
{
    public class Validator : ValidatorBase<Command>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsmin belirtilmesi zorunludur");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyismin belirtilmesi zorunludur");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı isminin belirtilmesi zorunludur");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrenin belirtilmesi zorunludur")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır");

            RuleFor(x => x.PasswordRepeat)
                .NotEmpty().WithMessage("Şifre tekrarının belirtilmesi zorunludur")
                .Equal(x => x.Password).WithMessage("Şifre tekrarı şifre ile aynı olmalıdır");
        }
    }
}
