using System.Text.Json.Serialization;

namespace EmirOtomotiv.Core.Domain.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }

    [JsonIgnore]
    public override DateTime UpdatedAt { get; set; }
}