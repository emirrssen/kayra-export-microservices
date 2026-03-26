using FluentValidation;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Exceptions;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Pipelines
{
    public class FluentValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, new()
        where TResponse : BaseResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public FluentValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ValidationContext<TRequest> context = new(request);
            var validatorResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = validatorResult.SelectMany(x => x.Errors).ToList();

            if (failures.Any())
                throw new ValidatorException(string.Join("\n", failures.Select(x => x.ErrorMessage).Distinct()));

            return await next();
        }
    }
}
