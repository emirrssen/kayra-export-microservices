namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Exceptions
{
    public sealed class ValidatorException(string message) : Exception(message);
}
