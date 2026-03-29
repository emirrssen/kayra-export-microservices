namespace KayraExport.Microservices.Services.Product.Domain.Events
{
    public class ProductCreatedEvent
    {
        public DateTime CreatedAt { get; set; }
        public long CreatedProductId { get; set; }
    }
}
