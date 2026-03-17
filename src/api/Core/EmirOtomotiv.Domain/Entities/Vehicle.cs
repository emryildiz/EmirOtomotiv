namespace EmirOtomotiv.Core.Domain.Entities;

public class Vehicle : BaseEntity
{
    public required string Name { get; set; }

    public required string Model { get; set; }

    public required string Year { get; set; }
}