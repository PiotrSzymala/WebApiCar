namespace WebApiCar.Infrastructure.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = default!;
        public string RegistryPlate { get; set; } = default!;
        public string VinNumber { get; set; } = default!;
        public bool IsAvailable { get; set; }
    }
}