namespace ConstructionEquipmentRental.Services.Options
{
    public record struct EquipmentListOptions
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
    }
}
