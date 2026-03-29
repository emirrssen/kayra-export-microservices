using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.FluentValidation;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Delete
{
    public class Validator : ValidatorBase<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ürün Id'si belirtilmelidir");
        }
    }
}
