using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Entities
{
    /// <summary>
    /// Postgresql entity'lerinde standart kullanılan field değerlerini getirmek için kullanılır.
    /// </summary>
    public class BasePostgreSqlEntity : BaseEntity
    {
        public long Id { get; set; }
        [Column(TypeName = "timestamp without time zone")] public DateTime CreatedAt { get; set; }
        [Column(TypeName = "timestamp without time zone")] public DateTime? UpdatedAt { get; set; }
        [Column(TypeName = "timestamp without time zone")] public DateTime? DeletedAt { get; set; }
    }
}
