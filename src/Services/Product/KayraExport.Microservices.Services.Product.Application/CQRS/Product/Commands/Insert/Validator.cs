using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.FluentValidation;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Insert
{
    public class Validator : ValidatorBase<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün isminin belirtilmesi zorunludur");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat değeri 0'dan küçük olamaz");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı 0'dan küçük olamaz");
        }
    }
}
