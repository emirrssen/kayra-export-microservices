using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Core.Abstraction.MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Abstraction.FluentValidation
{
    /// <summary>
    /// Tüm projede kullanılan validator'leri soyutlayan sınıftır. Yalnıcza proje bazında 
    /// query ve command'leri soyutlamak için oluşturulan base sınıfları kalıtan query ve command'ler de kullanılabilir.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    public class ValidatorBase<TQuery> : AbstractValidator<TQuery> where TQuery : IMediatrAbstractionCore { }
}
